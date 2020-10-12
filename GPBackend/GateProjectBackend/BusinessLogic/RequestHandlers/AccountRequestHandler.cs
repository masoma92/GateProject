using GateProjectBackend.BusinessLogic.RequestHandlers.Requests;
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
    public class AccountRequestHandler : IRequestHandler<GetAllAccountsRequest, Result<ListResult<Account>>>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountRequestHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Result<ListResult<Account>>> Handle(GetAllAccountsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _accountRepository.GetList(request.PaginationEntry, request.Sorting, request.Filtering);

                return Result<ListResult<Account>>.Ok(result);
            }
            catch (Exception e)
            {
                return Result<ListResult<Account>>.Failure(e.Message);
            }
        }
    }
}
