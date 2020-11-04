using GateProjectBackend.BusinessLogic.CommandHandlers.Commands.Dashboard;
using GateProjectBackend.BusinessLogic.RequestHandlers.Requests;
using GateProjectBackend.BusinessLogic.RequestHandlers.Requests.Dashboard;
using GateProjectBackend.Common;
using GateProjectBackend.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class DashboardController : BaseController
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Admin actions

        [RoleBasedAuthorize(AcceptedRoles = "Admin")]
        [HttpGet("sumAccounts")]
        public async Task<IActionResult> SumAccounts()
        {
            var request = new SumAccounts();

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [RoleBasedAuthorize(AcceptedRoles = "Admin")]
        [HttpGet("sumGates")]
        public async Task<IActionResult> SumGates()
        {
            var request = new SumGates();

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [RoleBasedAuthorize(AcceptedRoles = "Admin")]
        [HttpGet("sumUsers")]
        public async Task<IActionResult> SumUsers()
        {
            var request = new SumUsers();

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [RoleBasedAuthorize(AcceptedRoles = "Admin")]
        [HttpGet("sumErrors")]
        public async Task<IActionResult> SumErrors()
        {
            var request = new SumErrors();

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [RoleBasedAuthorize(AcceptedRoles = "Admin")]
        [HttpPost("createAccountChart")]
        public async Task<IActionResult> CreateAccountChart([FromBody] CreateAccountChart command)
        {
            var result = await _mediator.Send(command);

            return StatusCodeResult(result);
        }

        // AccountAdmin sctions
        [HttpGet("sumGatesByAccount/{id}")]
        public async Task<IActionResult> SumGatesByAccount(int id)
        {
            var email = HttpContext.User.Identity.Name;
            var request = new SumGatesByAccount { AccountId = id, RequestedEmail = email };

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [HttpGet("sumUsersByAccount/{id}")]
        public async Task<IActionResult> SumUsersByAccount(int id)
        {
            var email = HttpContext.User.Identity.Name;
            var request = new SumUsersByAccount { AccountId = id, RequestedEmail = email };

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [HttpGet("sumErrorsByAccount/{id}")]
        public async Task<IActionResult> SumErrorsByAccount(int id)
        {
            var email = HttpContext.User.Identity.Name;
            var request = new SumErrorsByAccount { AccountId = id, RequestedEmail = email };

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [HttpGet("sumAdminsByAccount/{id}")]
        public async Task<IActionResult> SumAdminsByAccount(int id)
        {
            var email = HttpContext.User.Identity.Name;
            var request = new SumAdminsByAccount { AccountId = id, RequestedEmail = email };

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [HttpPost("get-enters")]
        public async Task<IActionResult> GetEnters([FromBody] GetEnters request)
        {

            var pagination = HttpContext.Request.Headers["x-pagination"].FirstOrDefault() ?? "{}";
            request.PaginationEntry = JsonConvert.DeserializeObject<PaginationEntry>(pagination);

            var sorting = HttpContext.Request.Headers["x-sorting"].FirstOrDefault() ?? "{}";
            request.Sorting = JsonConvert.DeserializeObject<Sorting>(sorting);

            var filtering = HttpContext.Request.Headers["x-filtering"].FirstOrDefault() ?? "";
            request.Filtering = filtering;

            request.RequestedEmail = HttpContext.User.Identity.Name;

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        // User actions

        [HttpGet("lastGateAccessDate")]
        public async Task<IActionResult> LastGateAccessDate()
        {
            var email = HttpContext.User.Identity.Name;
            var request = new GetLastGateAccessDate { RequestedEmail = email };

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [HttpGet("registrationDate")]
        public async Task<IActionResult> RegistrationDate()
        {
            var email = HttpContext.User.Identity.Name;
            var request = new GetRegDate { RequestedEmail = email };

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [HttpGet("sumGateAdminAccesses")]
        public async Task<IActionResult> SumGateAdminAccesses()
        {
            var email = HttpContext.User.Identity.Name;
            var request = new SumGateAdminAccesses { RequestedEmail = email };

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [HttpGet("sumGateAccesses")]
        public async Task<IActionResult> SumGateAccesses()
        {
            var email = HttpContext.User.Identity.Name;
            var request = new SumGateAccesses { RequestedEmail = email };

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [HttpPost("createGateUsageChart")]
        public async Task<IActionResult> CreateGateUsageChart([FromBody] CreateGateUsageChart command)
        {
            command.RequestedBy = HttpContext.User.Identity.Name;

            var result = await _mediator.Send(command);

            return StatusCodeResult(result);
        }
    }
}
