using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers.Commands
{
    public class EntryCommand : IRequest<Result<bool>>
    {
        [Required]
        public string CharacteristicId { get; set; }
        [Required]
        public string ServiceId { get; set; }
        public string Email { get; set; }
    }
}
