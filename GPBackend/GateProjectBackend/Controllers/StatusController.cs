using GateProjectBackend.BusinessLogic.RequestHandlers.Requests;
using GateProjectBackend.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class StatusController : BaseController
    {
        private readonly IMediator _mediator;

        public StatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // return role and account admin status!
        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            var result = await _mediator.Send(new GetMyRoleRequest { Email = HttpContext.User.Identity.Name });

            return StatusCodeResult(result);
        }
    }
}
