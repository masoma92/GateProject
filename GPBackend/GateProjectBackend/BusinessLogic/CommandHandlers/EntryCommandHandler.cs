﻿using GateProjectBackend.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Common;
using GateProjectBackend.Data.Repositories;
using GateProjectBackend.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers
{
    public class EntryCommandHandler : 
        IRequestHandler<JwtEntryCommand, Result<bool>>,
        IRequestHandler<RfidEntryCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGateRepository _userGateRepository;
        private readonly IGateRepository _gateRepository;
        private readonly ILogService _logService;

        public EntryCommandHandler(IUserRepository userRepository,
            IUserGateRepository userGateRepository,
            IGateRepository gateRepository,
            ILogService logService)
        {
            _userRepository = userRepository;
            _userGateRepository = userGateRepository;
            _gateRepository = gateRepository;
            _logService = logService;
        }
        public async Task<Result<bool>> Handle(JwtEntryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var access = await CheckAccess(request.Email, request.GateId);

                return Result<bool>.Ok(access);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }

        public async Task<Result<bool>> Handle(RfidEntryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var email = TokenHelper.Decrypt(request.RfidKey);

                var access = await CheckAccess(email, request.GateId);

                return Result<bool>.Ok(access);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }

        private async Task<bool> CheckAccess(string email, int gateId)
        {
            var user = await _userRepository.GetUserByEmail(email);
            var gate = await _gateRepository.Get(gateId);


            var result = await _userGateRepository.CheckAccess(gate.Id, user.Id);

            if (result)
                await _logService.Create("success", EventTypes.Enter, user.Id, gate.AccountId, gate.Id);
            else
                await _logService.Create("failed", EventTypes.Enter, user.Id, gate.AccountId, gate.Id);

            return result;
        }
    }
}
