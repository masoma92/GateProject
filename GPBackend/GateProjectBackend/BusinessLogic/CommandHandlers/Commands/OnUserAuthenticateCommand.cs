using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers.Commands
{
    public class OnUserAuthenticateCommand : IRequest<Result<bool>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime Birth { get; set; }
    }
}
