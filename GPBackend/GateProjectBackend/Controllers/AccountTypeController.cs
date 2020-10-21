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
    public class AccountTypeController : BaseController
    {
        private readonly IMediator _mediator;

        public AccountTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAccountTypes()
        {
            var request = new GetAllAccountTypesRequest();

            var result = await _mediator.Send(request);

            return StatusCodeResult(result);
        }
    }
}
