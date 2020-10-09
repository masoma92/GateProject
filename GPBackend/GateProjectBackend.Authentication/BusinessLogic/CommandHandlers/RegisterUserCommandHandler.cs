using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Authentication.BusinessLogic.Helpers;
using GateProjectBackend.Authentication.Data.Models;
using GateProjectBackend.Authentication.Data.Repositories;
using GateProjectBackend.Authentication.Resources;
using GateProjectBackend.Authentication.Resources.Settings;
using GateProjectBackend.Common;
using MediatR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.Handlers
{
    public class RegisterUserCommandHandler : 
        IRequestHandler<RegisterUserCommand, Result<bool>>,
        IRequestHandler<ConfirmEmailCommand, Result<bool>>,
        IRequestHandler<ResendConfirmEmailCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<SendGridEmailVariables> _companyProperties;
        private readonly IOptions<SendGridEmailSettings> _sendGridEmailSettings;
        private readonly IOptions<UrlSettings> _urlSettings;
        private readonly IEmailSender _emailSender;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IOptions<SendGridEmailVariables> companyProperties,
            IOptions<SendGridEmailSettings> sendGridEmailSettings,
            IOptions<UrlSettings> urlSettings,
            IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _companyProperties = companyProperties;
            _sendGridEmailSettings = sendGridEmailSettings;
            _emailSender = emailSender;
            _urlSettings = urlSettings;
        }


        public async Task<Result<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Password2))
                    return Result<bool>.BadRequest("Password is required");

                if (request.Password != request.Password2)
                    return Result<bool>.BadRequest("Passwords are not the same");

                var user = await _userRepository.GetUserByEmail(request.Email);
                if (user != null)
                    return Result<bool>.BadRequest($"Email {request.Email} is already taken!");

                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var newUser = await _userRepository.CreateUser(request.FirstName, request.LastName, request.Email, request.Birth, passwordHash, passwordSalt);

                var confirmationToken = TokenHelper.GenerateToken(newUser.PasswordSalt);

                SendConfirmationEmail(request.Email, $"{newUser.FirstName} {newUser.LastName}", confirmationToken);

                return Result<bool>.Ok(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
            
        }

        public async Task<Result<bool>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.Email);
                if (user == null)
                    return Result<bool>.BadRequest($"User with {request.Email} email doesn't exist!");

                if (user.IsConfirmed)
                    return Result<bool>.BadRequest($"User is already activated!");

                var isValidToken = TokenHelper.IsValidToken(request.Token, user.PasswordSalt);

                if (!isValidToken)
                    return Result<bool>.BadRequest($"Activation link is not valid or expired!");

                user.IsConfirmed = true;

                await _userRepository.Update(user);

                return Result<bool>.Ok(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }

        public async Task<Result<bool>> Handle(ResendConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.Email);
                if (user == null)
                    return Result<bool>.BadRequest($"User doesn't exist");

                var confirmationToken = TokenHelper.GenerateToken(user.PasswordSalt);

                SendConfirmationEmail(request.Email, $"{user.FirstName} {user.LastName}", confirmationToken);

                return Result<bool>.Ok(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private void SendConfirmationEmail(string email, string displayName, string token)
        {
            var variables = new SendGridEmailVariables()
            {
                AuthWebUrl = _urlSettings.Value.AuthWebUrl,
                CompanyEmail = _companyProperties.Value.CompanyEmail,
                CompanyName = _companyProperties.Value.CompanyName,
                TemplateId = _sendGridEmailSettings.Value.ConfirmEmailTemplateId,
                ToAddress = email.ToLower(),
                Token = token,
                ToName = displayName
            };

            _emailSender.SendAsync(variables).Wait();
        }
    }
}
