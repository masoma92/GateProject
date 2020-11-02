using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Responses.Dashboard
{
    public class GetEntersResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime FirstUse { get; set; }
        public DateTime LastUse { get; set; }
        public string GateName { get; set; }
    }
}
