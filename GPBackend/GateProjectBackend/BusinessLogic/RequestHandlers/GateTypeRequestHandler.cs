using GateProjectBackend.BusinessLogic.RequestHandlers.Requests;
using GateProjectBackend.BusinessLogic.RequestHandlers.Responses;
using GateProjectBackend.Common;
using GateProjectBackend.Data.Models;
using GateProjectBackend.Data.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers
{
    public class GateTypeRequestHandler : IRequestHandler<GetAllGateTypesRequest, Result<ListResult<GateTypeResponse>>>
    {
        private readonly IGateTypeRepository _gateTypeRepository;

        public GateTypeRequestHandler(IGateTypeRepository gateTypeRepository)
        {
            _gateTypeRepository = gateTypeRepository;
        }
        public async Task<Result<ListResult<GateTypeResponse>>> Handle(GetAllGateTypesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _gateTypeRepository.GetList();

                var response = CreateListResponse(result.Records.ToList());

                return Result<ListResult<GateTypeResponse>>.Ok(response);
            }
            catch (Exception e)
            {
                return Result<ListResult<GateTypeResponse>>.Failure(e.Message);
            }
        }

        private ListResult<GateTypeResponse> CreateListResponse(List<GateType> result)
        {
            List<GateTypeResponse> response = new List<GateTypeResponse>();
            foreach (var item in result)
            {
                response.Add(new GateTypeResponse
                {
                    Name = item.Name
                });
            }
            return new ListResult<GateTypeResponse>(response, response.Count);
        }
    }
}
