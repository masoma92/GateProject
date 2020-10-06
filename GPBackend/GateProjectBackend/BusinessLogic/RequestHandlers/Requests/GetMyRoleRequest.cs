using GateProjectBackend.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers.Requests
{
    public class GetMyRoleRequest : IRequest<Result<string>>
    {
        public string Email { get; set; }
    }
}
