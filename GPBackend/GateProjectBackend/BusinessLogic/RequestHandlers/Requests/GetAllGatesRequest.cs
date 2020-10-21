using GateProjectBackend.BusinessLogic.RequestHandlers.Responses;
using GateProjectBackend.Common;
using GateProjectBackend.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Requests
{
    public class GetAllGatesRequest : IRequest<Result<ListResult<GateResponse>>>
    {
        public PaginationEntry PaginationEntry { get; set; }
        public Sorting Sorting { get; set; }
        public string Filtering { get; set; }
        [JsonIgnore]
        public string RequestedUserName { get; set; }
    }
}
