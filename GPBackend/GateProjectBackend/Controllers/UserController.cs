using System;
using System.Linq;
using System.Threading.Tasks;
using GateProjectBackend.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GateProjectBackend.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v1/[controller]")]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("onUserAuthenticate")]
        public async Task<IActionResult> OnUserAuthenticate()
        {
            var command = GetUserClaims();
            var result = await _mediator.Send(command);

            return StatusCodeResult(result);
        }

        private OnUserAuthenticateCommand GetUserClaims()
        {
            var fname = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "firstname").Value;
            var lname = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "lastname").Value;
            var email = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            var birth = DateTime.Parse(this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/dateofbirth").Value);
            return new OnUserAuthenticateCommand
            {
                FirstName = fname,
                LastName = lname,
                Email = email,
                Birth = birth
            };
        }
    }
}
