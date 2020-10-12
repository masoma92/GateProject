using System;
using System.Linq;
using System.Threading.Tasks;
using GateProjectBackend.BusinessLogic.RequestHandlers.Requests;
using GateProjectBackend.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GateProjectBackend.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v1/[controller]")]
    [RoleBasedAuthorize(AcceptedRoles = "Admin, User")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetUsers()
        {
            var request = new GetAllUsersRequest();

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }
    }
}
