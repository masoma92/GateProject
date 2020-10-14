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
    public class AccountTypeRequestHandler : IRequestHandler<GetAllAccountTypesRequest, Result<ListResult<AccountTypeResponse>>>
    {
        private readonly IAccountTypeRepository _accountTypeRepository;

        public AccountTypeRequestHandler(IAccountTypeRepository accountTypeRepository)
        {
            _accountTypeRepository = accountTypeRepository;
        }
        public async Task<Result<ListResult<AccountTypeResponse>>> Handle(GetAllAccountTypesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _accountTypeRepository.GetList();

                var response = CreateListResponse(result.Records.ToList());

                return Result<ListResult<AccountTypeResponse>>.Ok(response);
            }
            catch (Exception e)
            {
                return Result<ListResult<AccountTypeResponse>>.Failure(e.Message);
            }
        }

        private ListResult<AccountTypeResponse> CreateListResponse(List<AccountType> result)
        {
            List<AccountTypeResponse> response = new List<AccountTypeResponse>();
            foreach (var item in result)
            {
                response.Add(new AccountTypeResponse
                {
                    Name = item.Name
                });
            }
            return new ListResult<AccountTypeResponse>(response, response.Count);
        }
    }
}
