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
        private readonly IAccountAdminRepository _accountAdminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAccountUserRepository _accountUserRepository;

        public AccountRequestHandler(IAccountRepository accountRepository,
            IAccountAdminRepository accountAdminRepository,
            IUserRepository userRepository,
            IAccountUserRepository accountUserRepository)
        {
            _accountRepository = accountRepository;
            _accountAdminRepository = accountAdminRepository;
            _userRepository = userRepository;
            _accountUserRepository = accountUserRepository;
        }
        public async Task<Result<ListResult<AccountResponse>>> Handle(GetAllAccountsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.RequestEmail);

                if (user.Role.Name != "Admin" && !IsAccountAdmin(user.Id))
                {
                    return Result<ListResult<AccountResponse>>.AccessDenied("No access!");
                }

                var result = await _accountRepository.GetList(request.PaginationEntry, request.Sorting, request.Filtering);

                if (user.Role.Name != "Admin")
                {
                    var records = result.Records;
                    var newRecords = records.ToList().Where(x => x.Admins.Select(x => x.UserId).Contains(user.Id));
                    result = new ListResult<Account>(newRecords, newRecords.Count());
                }

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
                var user = await _userRepository.GetUserByEmail(request.RequestEmail);
                var isAdminOfAccount = await _accountAdminRepository.IsAdminOfAccount(user.Id, request.Id);

                if (user.Role.Name != "Admin" && !isAdminOfAccount)
                {
                    return Result<AccountResponse>.AccessDenied("No access!");
                }

                var account = await _accountRepository.Get(request.Id);

                var admins = await _accountAdminRepository.GetAllUsersByAccountId(request.Id);

                var users = await _accountUserRepository.GetAllUsersByAccountId(request.Id);

                var response = CreateResponse(account, admins, users);

                return Result<AccountResponse>.Ok(response);
            }
            catch (Exception e)
            {
                return Result<AccountResponse>.Failure(e.Message);
            }
        }

        private AccountResponse CreateResponse(Account account, IEnumerable<User> admins, IEnumerable<User> users)
        {
            AccountResponse response = new AccountResponse
            {
                Id = account.Id,
                AccountType = account.AccountType.Name,
                City = account.City,
                Name = account.Name,
                ContactEmail = account.ContactEmail,
                Country = account.Country,
                Street = account.Street,
                StreetNo = account.StreetNo,
                Zip = account.Zip
            };
            response.AdminEmails = new List<string>();
            response.UserEmails = new List<string>();
            foreach (var admin in admins)
            {
                response.AdminEmails.Add(admin.Email);
            }
            foreach (var user in users)
            {
                response.UserEmails.Add(user.Email);
            }
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

        private bool IsAccountAdmin(int userId)
        {
            return _accountAdminRepository.IsAccountAdmin(userId).Result;
        }
    }
}
