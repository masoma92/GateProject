using GateProjectBackend.BusinessLogic.RequestHandlers.Responses;
using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Requests
{
    public class GetAllGateTypesRequest : IRequest<Result<ListResult<GateTypeResponse>>>
    {
    }
}
