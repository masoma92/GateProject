using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Authentication.BusinessLogic.Helpers;
using GateProjectBackend.Authentication.BusinessLogic.Responses;
using GateProjectBackend.Authentication.Data.Models;
using GateProjectBackend.Authentication.Data.Repositories;
using GateProjectBackend.Authentication.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<RegisterUserResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public RegisterUserCommandHandler(
            IUserRepository userRepository,
            JwtSettings jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
        }

        public async Task<Result<RegisterUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.Password2))
                    return Result<RegisterUserResponse>.BadRequest("Password is required");

                if (request.Password != request.Password2)
                    return Result<RegisterUserResponse>.BadRequest("Passwords are not the same");

                var user = _userRepository.GetUserByEmail(request.Email);
                if (user != null)
                    return Result<RegisterUserResponse>.BadRequest($"Email {request.Email} is already taken!");

                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var newUser = await _userRepository.CreateUser(request.FirstName, request.LastName, request.Email, passwordHash, passwordSalt);

                // confirmation email sender!!!
                var confirmationToken = GenerateConfirmationToken(newUser.Id, newUser.Email);

                var userResponse = Convert(newUser);
                return Result<RegisterUserResponse>.Ok(userResponse);
            }
            catch (Exception e)
            {
                return Result<RegisterUserResponse>.Failure(e.Message);
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

        private string GenerateConfirmationToken(int userId, string email)
        {
            string data = $"{userId}{email}{_jwtSettings.Secret}";
            using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

            // Convert byte array to a string   
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
