using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Requests
{
    public class SumGateAccesses : IRequest<Result<int>>
    {
        [JsonIgnore]
        public string RequestedEmail { get; set; }
    }
}
