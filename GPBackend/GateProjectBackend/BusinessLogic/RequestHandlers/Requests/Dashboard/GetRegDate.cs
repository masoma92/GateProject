using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Requests.Dashboard
{
    public class GetRegDate : IRequest<Result<DateTime>>
    {
        [JsonIgnore]
        public string RequestedEmail { get; set; }
    }
}
