using GateProjectBackend.Common;
using GateProjectBackend.Data.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Requests
{
    public class GetAllUsersRequest : IRequest<Result<IEnumerable<User>>>
    {
    }
}
