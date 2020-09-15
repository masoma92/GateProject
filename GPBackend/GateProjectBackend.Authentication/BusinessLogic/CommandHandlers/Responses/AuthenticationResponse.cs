using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Responses
{
    public class AuthenticationResponse
    {
        public bool Success { get; set; }
        public string JwtToken { get; set; }
    }
}
