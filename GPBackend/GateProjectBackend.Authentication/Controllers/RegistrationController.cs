using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GateProjectBackend.Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerCommand)
        {
            var result = await _mediator.Send(registerCommand);
            return Ok(result);
        }
    }
}
