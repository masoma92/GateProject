using GateProjectBackend.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v1/[controller]")]
    [RoleBasedAuthorize(AcceptedRoles = "Admin, User")]
    public class EntryController : BaseController
    {
        private readonly IMediator _mediator;

        public EntryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<IActionResult> Enter([FromBody] EntryCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.Email = HttpContext.User.Identity.Name;

            var result = await _mediator.Send(command);

            return StatusCodeResult(result);
        }
    }
}
