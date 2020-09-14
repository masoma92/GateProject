using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Authentication.BusinessLogic.Helpers;
using GateProjectBackend.Authentication.BusinessLogic.Responses;
using GateProjectBackend.Authentication.Data.Models;
using GateProjectBackend.Authentication.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<RegisterUserResponse>>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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

                string activationToken = "";

                var newUser = await _userRepository.CreateUser(request.FirstName, request.LastName, request.Email, passwordHash, passwordSalt, activationToken);

                // confirmation email sender!!!

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

        private string GenerateConfirmationToken(RegisterUserCommand request)
        {

        }
    }
}
