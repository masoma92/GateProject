using GateProjectBackend.BusinessLogic.RequestHandlers.Requests;
using GateProjectBackend.Common;
using GateProjectBackend.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers
{
    public class RoleRequestHandler : IRequestHandler<GetMyRoleRequest, Result<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public RoleRequestHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public async Task<Result<string>> Handle(GetMyRoleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.Email);
                if (user == null)
                    return Result<string>.BadRequest("User doesn't exist!");
                var role = await _roleRepository.GetRoleByUserEmail(request.Email);

                return Result<string>.Ok(role.Name);
            }
            catch (Exception e)
            {
                return Result<string>.Failure(e.Message);
            }
        }
    }
}
