﻿using GateProjectBackend.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.BusinessLogic.RequestHandlers.Requests;
using GateProjectBackend.Common;
using GateProjectBackend.Resources;
using MediatR;
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
    [RoleBasedAuthorize(AcceptedRoles = "Admin")]
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAccounts()
        {
            var request = new GetAllAccountsRequest();

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

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.CreatedBy = HttpContext.User.Identity.Name;

            var result = await _mediator.Send(command);

            return StatusCodeResult(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.ModifiedBy = HttpContext.User.Identity.Name;

            var result = await _mediator.Send(command);

            return StatusCodeResult(result);
        }
    }
}
