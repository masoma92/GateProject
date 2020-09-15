using GateProjectBackend.Authentication.BusinessLogic.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands
{
    public class ConfirmEmailCommand : IRequest<Result<bool>>
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
