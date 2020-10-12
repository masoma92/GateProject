using System;
using System.Linq;
using System.Threading.Tasks;
using GateProjectBackend.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GateProjectBackend.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v1/[controller]")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [RoleBasedAuthorize(AcceptedRoles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            return Ok();
        }
    }
}
