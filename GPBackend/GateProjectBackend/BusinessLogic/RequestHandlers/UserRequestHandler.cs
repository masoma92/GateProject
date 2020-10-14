using GateProjectBackend.BusinessLogic.RequestHandlers.Requests;
using GateProjectBackend.BusinessLogic.RequestHandlers.Responses;
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
    public class UserRequestHandler : IRequestHandler<GetAllUsersRequest, Result<ListResult<UserResponse>>>
    {
        private readonly IUserRepository _userRepository;

        public UserRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<ListResult<UserResponse>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userRepository.GetAll();

                var response = CreateListResponse(result.Records.ToList());

                return Result<ListResult<UserResponse>>.Ok(response);
            }
            catch (Exception e)
            {
                return Result<ListResult<UserResponse>>.Failure(e.Message);
            }
        }

        private ListResult<UserResponse> CreateListResponse(List<User> result)
        {
            List<UserResponse> response = new List<UserResponse>();
            foreach (var item in result)
            {
                response.Add(new UserResponse
                {
                    Email = item.Email
                });
            }
            return new ListResult<UserResponse>(response, response.Count);
        }
    }
}
