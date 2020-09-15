using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Authentication.BusinessLogic.Helpers;
using GateProjectBackend.Authentication.BusinessLogic.Shared;
using GateProjectBackend.Authentication.Data.Repositories;
using GateProjectBackend.Authentication.Resources.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.CommandHandlers
{
    public class ResetPasswordCommandHandler : 
        IRequestHandler<ForgetPasswordCommand, Result<bool>>,
        IRequestHandler<ResetPasswordCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<SendGridEmailVariables> _companyProperties;
        private readonly IOptions<UrlSettings> _urlSettings;
        private readonly IEmailSender _emailSender;

        public ResetPasswordCommandHandler(
            IUserRepository userRepository,
            IOptions<SendGridEmailVariables> companyProperties,
            IOptions<UrlSettings> urlSettings,
            IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _companyProperties = companyProperties;
            _urlSettings = urlSettings;
            _emailSender = emailSender;
        }

        public async Task<Result<bool>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.Email);
                if (user == null)
                    return Result<bool>.BadRequest($"User with {request.Email} email doesn't exist!");

                if (!user.IsConfirmed)
                    return Result<bool>.BadRequest($"User is not activated!");

                var token = TokenHelper.GenerateToken(user.PasswordSalt);

                SendResetEmail(user.Email, $"{user.FirstName} {user.LastName}", token);

                return Result<bool>.Ok(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }

        public async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Password2))
                    return Result<bool>.BadRequest("Password is required");

                if (request.Password != request.Password2)
                    return Result<bool>.BadRequest("Passwords are not the same");

                var user = await _userRepository.GetUserByEmail(request.Email);
                if (user == null)
                    return Result<bool>.BadRequest($"User with {request.Email} email doesn't exist!");

                if (!user.IsConfirmed)
                    return Result<bool>.BadRequest($"User is not activated!");

                var isTokenValid = TokenHelper.IsValidToken(request.Token, user.PasswordSalt);

                if (!isTokenValid)
                    return Result<bool>.BadRequest("Token is invalid!");

                CreatePasswordHash(request.Password, out var newPasswordHash, out var newPasswordSalt);

                user.PasswordHash = newPasswordHash;
                user.PasswordSalt = newPasswordSalt;

                await _userRepository.Update(user);

                return Result<bool>.Ok(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }

        private void SendResetEmail(string email, string displayName, string token)
        {
            var variables = new SendGridEmailVariables()
            {
                AuthWebUrl = _urlSettings.Value.AuthWebUrl,
                CompanyEmail = _companyProperties.Value.CompanyEmail,
                CompanyName = _companyProperties.Value.CompanyName,
                TemplateId = "d-e0433dc85f80493e9afe020abae1e3cd",
                ToAddress = email.ToLower(),
                Token = token,
                ToName = displayName
            };

            _emailSender.SendAsync(variables).Wait();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}
