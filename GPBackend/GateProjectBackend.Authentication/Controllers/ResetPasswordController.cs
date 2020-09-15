using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Authentication.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResetPasswordController : AuthControllerBase
    {
        private readonly IMediator _mediator;
        public ResetPasswordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("forget")]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordCommand forgetPasswordCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(forgetPasswordCommand);
            return StatusCodeResult(result);
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand resetPasswordCommand)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(resetPasswordCommand);
            return StatusCodeResult(result);
        }
    }
}
