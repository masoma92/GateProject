using System.Threading.Tasks;
using GateProjectBackend.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GateProjectBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> OnUserAuthenticate()
        {
            var headers = this.Request.Headers;
            if (!headers.ContainsKey("Authorization")) return BadRequest("Authorization token not found");

            headers.TryGetValue("Authorization", out var token);
            var result = await _mediator.Send(new OnUserAuthenticateCommand { Token = token });

            return StatusCodeResult(result);
        }
    }
}
