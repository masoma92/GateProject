using GateProjectBackend.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Common;
using GateProjectBackend.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers
{
    public class EntryCommandHandler : IRequestHandler<JwtEntryCommand, Result<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGateRepository _userGateRepository;
        private readonly IGateRepository _gateRepository;

        public EntryCommandHandler(IUserRepository userRepository,
            IUserGateRepository userGateRepository,
            IGateRepository gateRepository)
        {
            _userRepository = userRepository;
            _userGateRepository = userGateRepository;
            _gateRepository = gateRepository;
        }
        public async Task<Result<bool>> Handle(JwtEntryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.Email);
                var gate = await _gateRepository.Get(request.GateId);

                var access = await _userGateRepository.CheckAccess(gate.Id, user.Id);

                return Result<bool>.Ok(access);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }
    }
}
