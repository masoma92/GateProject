using GateProjectBackend.BusinessLogic.RequestHandlers.Requests;
using GateProjectBackend.BusinessLogic.RequestHandlers.Responses;
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
    public class RoleRequestHandler : IRequestHandler<GetMyRoleRequest, Result<GetMyRoleResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAccountAdminRepository _accountAdminRepository;

        public RoleRequestHandler(
            IUserRepository userRepository, 
            IRoleRepository roleRepository,
            IAccountAdminRepository accountAdminRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _accountAdminRepository = accountAdminRepository;
        }

        public async Task<Result<GetMyRoleResponse>> Handle(GetMyRoleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.Email);
                if (user == null)
                    return Result<GetMyRoleResponse>.BadRequest("User doesn't exist!");
                var role = await _roleRepository.GetRoleByUserEmail(request.Email);

                var isAccountAdmin = await _accountAdminRepository.IsAccountAdmin(user.Id);

                var response = new GetMyRoleResponse { Role = role.Name, IsAccountAdmin = isAccountAdmin };

                return Result<GetMyRoleResponse>.Ok(response);
            }
            catch (Exception e)
            {
                return Result<GetMyRoleResponse>.Failure(e.Message);
            }
        }
    }
}
