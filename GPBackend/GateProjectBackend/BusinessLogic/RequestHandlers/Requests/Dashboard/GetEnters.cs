using GateProjectBackend.BusinessLogic.RequestHandlers.Responses.Dashboard;
using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Requests.Dashboard
{
    public class GetEnters : IRequest<Result<ListResult<GetEntersResponse>>>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int AccountId { get; set; }
        [JsonIgnore]
        public PaginationEntry PaginationEntry { get; set; }
        [JsonIgnore]
        public Sorting Sorting { get; set; }
        [JsonIgnore]
        public string Filtering { get; set; }
        [JsonIgnore]
        public string RequestedEmail { get; set; }
        
    }
}
