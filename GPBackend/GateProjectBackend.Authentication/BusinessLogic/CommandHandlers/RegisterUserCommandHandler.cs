﻿using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Authentication.BusinessLogic.Helpers;
using GateProjectBackend.Authentication.BusinessLogic.Responses;
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
        IRequestHandler<RegisterUserCommand, Result<RegisterUserResponse>>,
        IRequestHandler<ConfirmEmailCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<SendGridEmailVariables> _companyProperties;
        private readonly IOptions<UrlSettings> _urlSettings;
        private readonly IEmailSender _emailSender;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            IOptions<SendGridEmailVariables> companyProperties,
            IOptions<UrlSettings> urlSettings,
            IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _companyProperties = companyProperties;
            _emailSender = emailSender;
            _urlSettings = urlSettings;
        }

        public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Password2))
                    return Result<RegisterUserResponse>.BadRequest("Password is required");

                if (request.Password != request.Password2)
                    return Result<RegisterUserResponse>.BadRequest("Passwords are not the same");

                var user = await _userRepository.GetUserByEmail(request.Email);
                if (user != null)
                    return Result<RegisterUserResponse>.BadRequest($"Email {request.Email} is already taken!");

                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var newUser = await _userRepository.CreateUser(request.FirstName, request.LastName, request.Email, request.Birth, passwordHash, passwordSalt);

                var confirmationToken = TokenHelper.GenerateToken(newUser.PasswordSalt);

                SendConfirmationEmail(request.Email, $"{newUser.FirstName} {newUser.LastName}", confirmationToken);

                var userResponse = Convert(newUser);
                return Result<RegisterUserResponse>.Ok(userResponse);
            }
            catch (Exception e)
            {
                return Result<RegisterUserResponse>.Failure(e.Message);
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

        private RegisterUserResponse Convert(AuthUser newUser)
        {
            return new RegisterUserResponse
            {
                Id = newUser.Id
            };
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
                TemplateId = "d-969eb763cf304e528ea2e65b4aeed35e",
                ToAddress = email.ToLower(),
                Token = token,
                ToName = displayName
            };

            _emailSender.SendAsync(variables).Wait();
        }
    }
}
