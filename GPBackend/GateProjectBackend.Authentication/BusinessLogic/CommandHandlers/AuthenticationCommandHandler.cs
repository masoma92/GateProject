using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Responses;
using GateProjectBackend.Authentication.BusinessLogic.Helpers;
using GateProjectBackend.Authentication.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.CommandHandlers
{
    public class AuthenticationCommandHandler : IRequestHandler<AuthenticationCommand, Result<AuthenticationResponse>>
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<Result<AuthenticationResponse>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
        {

        }
    }
}
