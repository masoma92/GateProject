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
                var token = handler.ReadJwtToken(jwt);

                var firstName = token.Claims.Select(x => x.Type == "firstname");
                var lastName = token.Claims.Select(x => x.Type == "lastname");
                var email = token.Claims.Select(x => x.Type == "email");
                var birth = token.Claims.Select(x => x.Type == "birthdate");

                await _userRepository.CreateUser("", "", "", DateTime.Now);
                return Result<bool>.Ok(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }
    }
}
