using GateProjectBackend.BusinessLogic.RequestHandlers.Responses;
using GateProjectBackend.Common;
using GateProjectBackend.Data.Models;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Requests
{
    public class GetAllAccountsRequest : IRequest<Result<ListResult<AccountResponse>>>
    {
        public PaginationEntry PaginationEntry { get; set; }
        public Sorting Sorting { get; set; }
        public string Filtering { get; set; }
        [JsonIgnore]
        public string RequestEmail { get; set; }
    }
}
