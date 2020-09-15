using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Responses;
using GateProjectBackend.Authentication.BusinessLogic.Helpers;
using GateProjectBackend.Authentication.Data.Models;
using GateProjectBackend.Authentication.Data.Repositories;
using GateProjectBackend.Authentication.Resources;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.CommandHandlers
{
    public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, Result<AuthenticationResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationCommandHandler(
            IUserRepository userRepository,
            JwtSettings jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
        }
        public async Task<Result<AuthenticationResponse>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                    return Result<AuthenticationResponse>.BadRequest($"Email or password field is empty!");

                var user = await _userRepository.GetUserByEmail(request.Email);
                if (user == null)
                    return Result<AuthenticationResponse>.BadRequest($"User with {request.Email} email doesn't exist!");

                if (!user.IsConfirmed)
                    return Result<AuthenticationResponse>.BadRequest($"Email is not confirmed!");

                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                    return Result<AuthenticationResponse>.BadRequest($"Password is not correct!");

                var token = GenerateTokenForUser(user);

                return Result<AuthenticationResponse>.Ok(new AuthenticationResponse { JwtToken = token, Success = true });

            }
            catch (Exception e)
            {
                return Result<AuthenticationResponse>.Failure(e.Message);
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private string GenerateTokenForUser(AuthUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
