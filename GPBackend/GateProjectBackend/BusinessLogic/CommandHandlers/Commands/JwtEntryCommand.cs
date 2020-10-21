using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers.Commands
{
    public class JwtEntryCommand : IRequest<Result<bool>>
    {
        [Required]
        public int GateId { get; set; }
        public string Email { get; set; } // kontrollerben kap értéket!!!
    }
}
