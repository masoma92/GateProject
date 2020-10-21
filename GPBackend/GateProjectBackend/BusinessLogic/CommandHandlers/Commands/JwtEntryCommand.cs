using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers.Commands
{
    public class JwtEntryCommand : IRequest<Result<bool>>
    {
        [Required]
        public int GateId { get; set; }
        [JsonIgnore]
        public string Email { get; set; }
    }
}
