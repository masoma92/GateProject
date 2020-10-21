using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Responses
{
    public class GetMyRoleResponse
    {
        public string Role { get; set; }
        public bool IsAccountAdmin { get; set; }
    }
}
