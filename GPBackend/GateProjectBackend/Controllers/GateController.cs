using GateProjectBackend.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.BusinessLogic.RequestHandlers.Requests;
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
    public class GateController : BaseController
    {
        private readonly IMediator _mediator;

        public GateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [RoleBasedAuthorize(AcceptedRoles = "Admin, User")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetGates()
        {
            var request = new GetAllGatesRequest();

            request.RequestedUserName = HttpContext.User.Identity.Name;

            var pagination = HttpContext.Request.Headers["x-pagination"].FirstOrDefault() ?? "{}";
            request.PaginationEntry = JsonConvert.DeserializeObject<PaginationEntry>(pagination);

            var sorting = HttpContext.Request.Headers["x-sorting"].FirstOrDefault() ?? "{}";
            request.Sorting = JsonConvert.DeserializeObject<Sorting>(sorting);

            var filtering = HttpContext.Request.Headers["x-filtering"].FirstOrDefault() ?? "";
            request.Filtering = filtering;
            //var decodedString = JsonInHttpHeader.Decode(filtering);

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }

        [RoleBasedAuthorize(AcceptedRoles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateGate([FromBody] CreateGateCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.CreatedBy = HttpContext.User.Identity.Name;

            var result = await _mediator.Send(command);

            return StatusCodeResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GateGate(int id)
        {
            var command = new GetGateRequest { Id = id };

            command.RequestedBy = HttpContext.User.Identity.Name;

            var result = await _mediator.Send(command);

            return StatusCodeResult(result);
        }
    }
}
