using GateProjectBackend.BusinessLogic.RequestHandlers.Requests;
using GateProjectBackend.Common;
using GateProjectBackend.Data.Models;
using GateProjectBackend.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers
{
    public class UserRequestHandler : IRequestHandler<GetAllUsersRequest, Result<IEnumerable<User>>>
    {
        private readonly IUserRepository _userRepository;

        public UserRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<IEnumerable<User>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userRepository.GetAll();

                return Result<IEnumerable<User>>.Ok(result);
            }
            catch (Exception e)
            {
                return Result<IEnumerable<User>>.Failure(e.Message);
            }
        }
    }
}
