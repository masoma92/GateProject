using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers.Responses.Dashboard
{
    public class ChartResponse
    {
        public Dictionary<string, int> ChartData { get; set; }
    }
}
