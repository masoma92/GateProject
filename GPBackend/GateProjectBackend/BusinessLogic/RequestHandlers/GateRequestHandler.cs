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
    public class GateRequestHandler : 
        IRequestHandler<GetAllGatesRequest, Result<ListResult<GateResponse>>>,
        IRequestHandler<GetGateRequest, Result<GateResponse>>
    {
        private readonly IGateRepository _gateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserGateRepository _userGateRepository;

        public GateRequestHandler(IGateRepository gateRepository,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUserGateRepository userGateRepository)
        {
            _gateRepository = gateRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userGateRepository = userGateRepository;
        }

        public async Task<Result<GateResponse>> Handle(GetGateRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.RequestedBy);
                var access = await _userGateRepository.CheckAccess(request.Id, user.Id);
                var adminAccess = await _userGateRepository.CheckAdminAccess(request.Id, user.Id) || user.Role.Name == "Admin";
                if (!access && user.Role.Name == "User")
                    return Result<GateResponse>.BadRequest("No access to this gate!");

                var gate = await _gateRepository.Get(request.Id);

                var response = CreateResponse(gate, adminAccess);

                return Result<GateResponse>.Ok(response);
            }
            catch (Exception e)
            {
                return Result<GateResponse>.Failure(e.Message);
            }
        }

        public async Task<Result<ListResult<GateResponse>>> Handle(GetAllGatesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var role = await _roleRepository.GetRoleByUserEmail(request.RequestedUserName);
                var user = await _userRepository.GetUserByEmail(request.RequestedUserName);
                List<Gate> gates = new List<Gate>();
                if (role.Name == "Admin")
                {
                    gates = _gateRepository.GetList(request.PaginationEntry, request.Sorting, request.Filtering).Result.Records.ToList();
                }
                if (role.Name == "User")
                {
                    var temp = await _userGateRepository.GetAllGatesByUserIdAndAccess(user.Id);

                    gates = temp.Select(x => x.Gate).ToList();
                }

                var response = CreateListResponse(gates);

                return Result<ListResult<GateResponse>>.Ok(response);
            }
            catch (Exception e)
            {
                return Result<ListResult<GateResponse>>.Failure(e.Message);
            }
        }

        private GateResponse CreateResponse(Gate gate, bool adminAccess = false)
        {
            GateResponse response = new GateResponse();
            response.Id = gate.Id;
            response.Name = gate.Name;
            response.GateTypeName = gate.GateType.Name;
            response.AccountName = gate.Account == null ? "" : gate.Account.Name;
            if (adminAccess)
            {
                response.ServiceId = gate.ServiceId;
                response.CharacteristicId = gate.CharacteristicId;
                response.Users = CollectUserGates(gate.Users);
            }

            return response;
        }

        private ListResult<GateResponse> CreateListResponse(List<Gate> result)
        {
            List<GateResponse> response = new List<GateResponse>();
            foreach (var item in result)
            {
                var usersByGateId = _userGateRepository.GetAllUsersByGateId(item.Id).Result;
                response.Add(new GateResponse
                {
                    Id = item.Id,
                    AccountName = item.Account == null ? "" : item.Account.Name,
                    CharacteristicId = item.CharacteristicId,
                    GateTypeName = item.GateType.Name,
                    ServiceId = item.ServiceId,
                    Users = CollectUserGates(usersByGateId.ToList())
                });
            }
            return new ListResult<GateResponse>(response, response.Count);
        }

        private List<UserGateResponse> CollectUserGates(List<UserGate> usergates)
        {
            var ugr = new List<UserGateResponse>();
            foreach (var item in usergates)
            {
                ugr.Add(new UserGateResponse { AccessRight = item.AccessRight, AdminRight = item.AdminRight, Email = item.User.Email });
            }
            return ugr;
        }
    }
}
