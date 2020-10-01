using GateProjectBackend.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Common;
using GateProjectBackend.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers
{
    public class UserCommandHandler : IRequestHandler<OnUserAuthenticateCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<bool>> Handle(OnUserAuthenticateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var jwt = request.Token.Replace("Bearer", "").Trim();
                var handler = new JwtSecurityTokenHandler();
                var claims = handler.ReadJwtToken(jwt).Claims.ToList();

                var firstName = claims.FirstOrDefault(x => x.Type == "firstname").Value;
                var lastName = claims.FirstOrDefault(x => x.Type == "lastname").Value;
                var email = claims.FirstOrDefault(x => x.Type == "email").Value;
                var birth = claims.FirstOrDefault(x => x.Type == "birthdate").Value;

                if (_userRepository.GetUserByEmail(email).Result != null)
                    return Result<bool>.Ok(true);

                await _userRepository.CreateUser(firstName, lastName, email, DateTime.Parse(birth));
                return Result<bool>.Ok(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }
    }
}
