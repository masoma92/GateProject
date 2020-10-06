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
    public class RoleController : BaseController
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("my-role")]
        public async Task<IActionResult> OnUserAuthenticate()
        {
            var email = GetUserEmail();
            var result = await _mediator.Send(new GetMyRoleRequest { Email = email });

            return StatusCodeResult(result);
        }

        private string GetUserEmail()
        {
            return this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
        }
    }


}
