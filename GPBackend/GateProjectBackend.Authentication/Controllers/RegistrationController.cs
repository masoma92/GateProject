using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Authentication.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GateProjectBackend.Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : AuthControllerBase
    {
        private readonly IMediator _mediator;
        public RegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(registerCommand);
            return StatusCodeResult(result);
        }
    }
}
