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
    public class AccountRequestHandler : IRequestHandler<GetAllAccountsRequest, Result<ListResult<AccountResponse>>>,
        IRequestHandler<GetAccountRequest, Result<AccountResponse>>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountRequestHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<Result<ListResult<AccountResponse>>> Handle(GetAllAccountsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _accountRepository.GetList(request.PaginationEntry, request.Sorting, request.Filtering);

                var response = CreateListResponse(result.Records.ToList());

                return Result<ListResult<AccountResponse>>.Ok(response);
            }
            catch (Exception e)
            {
                return Result<ListResult<AccountResponse>>.Failure(e.Message);
            }
        }

        public async Task<Result<AccountResponse>> Handle(GetAccountRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _accountRepository.Get(request.Id);

                var response = CreateResponse(result);

                return Result<AccountResponse>.Ok(response);
            }
            catch (Exception e)
            {
                return Result<AccountResponse>.Failure(e.Message);
            }
        }

        private AccountResponse CreateResponse(Account result)
        {
            AccountResponse response = new AccountResponse
            {
                Id = result.Id,
                AccountType = result.AccountType.Name,
                City = result.City,
                Name = result.Name,
                ContactEmail = result.ContactEmail,
                Country = result.Country,
                Street = result.Street,
                StreetNo = result.StreetNo,
                Zip = result.Zip
            };
            return response;
        }

        private ListResult<AccountResponse> CreateListResponse(List<Account> result)
        {
            List<AccountResponse> response = new List<AccountResponse>();
            foreach (var item in result)
            {
                response.Add(new AccountResponse
                {
                    Id = item.Id,
                    AccountType = item.AccountType.Name,
                    City = item.City,
                    Name = item.Name,
                    ContactEmail = item.ContactEmail,
                    Country = item.Country,
                    Street = item.Street,
                    StreetNo = item.StreetNo,
                    Zip = item.Zip
                });
            }
            return new ListResult<AccountResponse>(response, response.Count);
        }
    }
}
