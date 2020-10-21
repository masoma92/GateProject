using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers.Commands
{
    public class RfidEntryCommand : IRequest<Result<bool>>
    {
        public int GateId { get; set; }
        public string RfidKey { get; set; }
    }
}
