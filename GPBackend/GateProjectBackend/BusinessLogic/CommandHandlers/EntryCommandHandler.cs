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
    public class EntryCommandHandler : IRequestHandler<EntryCommand, Result<bool>>
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
        public async Task<Result<bool>> Handle(EntryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.Email);

                // TODO

                return Result<bool>.Ok(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }
    }
}
