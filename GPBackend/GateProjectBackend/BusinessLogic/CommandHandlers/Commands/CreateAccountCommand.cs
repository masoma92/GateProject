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
    public class CreateAccountCommand : IRequest<Result<bool>>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Zip { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string StreetNo { get; set; }
        [Required]
        public string AccountType { get; set; }
        [Required]
        public string ContactEmail { get; set; }
        [JsonIgnore]
        public string CreatedBy { get; set; }
    }
}
