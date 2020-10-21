using GateProjectBackend.BusinessLogic.RequestHandlers.Responses;
using GateProjectBackend.Common;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Requests
{
    public class GetAccountRequest : IRequest<Result<AccountResponse>>
    {
        public int Id { get; set; }
        [JsonIgnore]
        public string RequestEmail { get; set; }
    }
}
