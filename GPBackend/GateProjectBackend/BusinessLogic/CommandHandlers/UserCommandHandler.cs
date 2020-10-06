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
                if (_userRepository.GetUserByEmail(request.Email).Result != null)
                    return Result<bool>.Ok(true);

                await _userRepository.CreateUser(request.FirstName, request.LastName, request.Email, request.Birth);
                return Result<bool>.Ok(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }
    }
}
