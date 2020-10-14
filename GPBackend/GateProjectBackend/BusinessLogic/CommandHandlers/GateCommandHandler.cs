using GateProjectBackend.BusinessLogic.CommandHandlers.Commands;
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
    public class GateCommandHandler : IRequestHandler<CreateGateCommand, Result<bool>>
    {
        private readonly IGateRepository _gateRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IGateTypeRepository _gateTypeRepository;

        public GateCommandHandler(
            IGateRepository gateRepository,
            IAccountRepository accountRepository,
            IGateTypeRepository gateTypeRepository)
        {
            _gateRepository = gateRepository;
            _accountRepository = accountRepository;
            _gateTypeRepository = gateTypeRepository;
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
    }
}
