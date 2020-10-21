using GateProjectBackend.BusinessLogic.RequestHandlers.Responses;
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
    public class UpdateGateCommand : IRequest<Result<bool>>
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string GateTypeName { get; set; }
        public string AccountName { get; set; }
        public List<UserGateResponse> Users { get; set; }
        [JsonIgnore]
        public string ModifiedBy { get; set; }
    }
}
