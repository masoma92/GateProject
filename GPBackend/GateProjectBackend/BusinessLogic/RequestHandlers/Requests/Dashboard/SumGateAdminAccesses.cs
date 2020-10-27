using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Requests
{
    public class SumGateAdminAccesses : IRequest<Result<int>>
    {
        [JsonIgnore]
        public string RequestedEmail { get; set; }
    }
}
