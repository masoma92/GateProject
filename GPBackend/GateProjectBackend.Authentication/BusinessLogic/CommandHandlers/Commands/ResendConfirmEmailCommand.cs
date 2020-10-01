using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands
{
    public class ResendConfirmEmailCommand : IRequest<Result<bool>>
    {
        public string Email { get; set; }
    }
}
