using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Responses;
using GateProjectBackend.Authentication.BusinessLogic.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands
{
    public class AuthenticationCommand : IRequest<Result<AuthenticationResponse>>
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
