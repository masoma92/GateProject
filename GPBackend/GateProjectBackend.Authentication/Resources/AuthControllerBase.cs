using GateProjectBackend.Authentication.BusinessLogic.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.Resources
{
    public class AuthControllerBase : ControllerBase
    {
        protected ObjectResult StatusCodeResult<T>(Result<T> result)
        {
            return result.ErrorType switch
            {
                ErrorType.NoError => Ok(result),
                ErrorType.NotFound => NotFound(result),
                ErrorType.AccessDenied => StatusCode(StatusCodes.Status403Forbidden, result),
                ErrorType.BadRequest => StatusCode(StatusCodes.Status400BadRequest, result),
                ErrorType.System => StatusCode(StatusCodes.Status503ServiceUnavailable, result),
                _ => StatusCode(StatusCodes.Status500InternalServerError, result),
            };
        }
    }
}
