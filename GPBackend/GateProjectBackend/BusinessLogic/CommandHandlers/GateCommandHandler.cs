using GateProjectBackend.BusinessLogic.CommandHandlers.Commands;
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

namespace GateProjectBackend.BusinessLogic.CommandHandlers
{
    public class GateCommandHandler : IRequestHandler<CreateGateCommand, Result<bool>>,
        IRequestHandler<UpdateGateCommand, Result<bool>>,
        IRequestHandler<DeleteGateCommand, Result<bool>>
    {
        private readonly IGateRepository _gateRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IGateTypeRepository _gateTypeRepository;
        private readonly IUserGateRepository _userGateRepository;
        private readonly IUserRepository _userRepository;

        public GateCommandHandler(
            IGateRepository gateRepository,
            IAccountRepository accountRepository,
            IGateTypeRepository gateTypeRepository,
            IUserGateRepository userGateRepository,
            IUserRepository userRepository)
        {
            _gateRepository = gateRepository;
            _accountRepository = accountRepository;
            _gateTypeRepository = gateTypeRepository;
            _userGateRepository = userGateRepository;
            _userRepository = userRepository;
        }
        public async Task<Result<bool>> Handle(CreateGateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _accountRepository.GetAccountByName(request.AccountName);
                var gateType = await _gateTypeRepository.GetGateTypeByName(request.Name);
                var newGate = new Gate
                {
                    GateTypeId = gateType == null ? 1 : gateType.Id,
                    Name = request.Name,
                    ServiceId = Guid.NewGuid().ToString(),
                    CharacteristicId = Guid.NewGuid().ToString(),
                    CreatedBy = request.CreatedBy,
                    CreatedAt = DateTime.UtcNow
                };
                if (account != null)
                    newGate.AccountId = account.Id;
                var res = await _gateRepository.Create(newGate);
                return Result<bool>.Ok(res);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }

        public async Task<Result<bool>> Handle(UpdateGateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(command.ModifiedBy);
                var adminAccess = await _userGateRepository.CheckAdminAccess(command.Id, user.Id);
                if (!adminAccess && !user.Role.Name.Equals("Admin"))
                {
                    return Result<bool>.AccessDenied("You have no access to modify gate!");
                }

                var gateType = await _gateTypeRepository.GetGateTypeByName(command.GateTypeName);
                var account = await _accountRepository.GetAccountByName(command.AccountName);
                var currentGate = await _gateRepository.Get(command.Id);

                await UpdateUserGates(command.Id, command.ModifiedBy, command.Users);

                UpdateGateProperties(currentGate, command, gateType, account);

                var res = await _gateRepository.Update(currentGate);

                return Result<bool>.Ok(res);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }

        public async Task<Result<bool>> Handle(DeleteGateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _gateRepository.Delete(request.Id);

                return Result<bool>.Ok(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }

        private void UpdateGateProperties(Gate currentGate, UpdateGateCommand request, GateType gateType, Account account)
        {
            currentGate.Name = request.Name;
            currentGate.GateTypeId = gateType != null ? gateType.Id : currentGate.GateTypeId;
            currentGate.AccountId = account != null ? account.Id : currentGate.AccountId;
            currentGate.ModifiedBy = request.ModifiedBy;
            currentGate.MoidifiedAt = DateTime.UtcNow;
        }

        private async Task UpdateUserGates(int gateId, string modifyingUser, IEnumerable<UserGateResponse> userGates)
        {
            var currentGateUsers = _gateRepository.Get(gateId).Result.Users;
            var newGateUsers = new List<UserGate>();
            foreach (var userGate in userGates)
            {
                var user = await _userRepository.GetUserByEmail(userGate.Email);
                if (user != null)
                    newGateUsers.Add(new UserGate 
                    {   
                        UserId = user.Id, 
                        GateId = gateId, 
                        AccessRight = userGate.AccessRight,
                        AdminRight = userGate.AdminRight,
                        CreatedBy = modifyingUser, 
                        CreatedAt = DateTime.UtcNow 
                    });
            }
            await _userGateRepository.Update(currentGateUsers, newGateUsers);
        }
    }
}
