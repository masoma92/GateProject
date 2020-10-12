using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers.Commands
{
    public class DeleteAccountCommand : IRequest<Result<bool>>
    {
        [Required]
        public int Id { get; set; }
    }
}
