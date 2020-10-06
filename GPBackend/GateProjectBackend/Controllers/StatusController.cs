using GateProjectBackend.Resources;
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
        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            return Ok();
        }
    }
}
