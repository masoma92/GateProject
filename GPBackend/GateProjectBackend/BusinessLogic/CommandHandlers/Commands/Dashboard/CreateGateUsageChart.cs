using GateProjectBackend.BusinessLogic.CommandHandlers.Responses.Dashboard;
using GateProjectBackend.Common;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers.Commands.Dashboard
{
    public class CreateGateUsageChart : IRequest<Result<ChartResponse>>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        [JsonIgnore]
        public string RequestedBy { get; set; }
    }
}
