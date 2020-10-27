using GateProjectBackend.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Common;
using GateProjectBackend.Data.Models;
using GateProjectBackend.Data.Repositories;
using MediatR;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers
{
    public class AccountCommandHandler : 
        IRequestHandler<CreateAccountCommand, Result<bool>>,
        IRequestHandler<UpdateAccountCommand, Result<bool>>,
        IRequestHandler<DeleteAccountCommand, Result<bool>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAccountAdminRepository _accountAdminRepository;
        private readonly IAccountUserRepository _accountUserRepository;

        public AccountCommandHandler(
            IAccountRepository accountRepository,
            IAccountTypeRepository accountTypeRepository,
            IUserRepository userRepository,
            IAccountAdminRepository accountAdminRepository,
            IAccountUserRepository accountUserRepository)
        {
            _accountRepository = accountRepository;
            _accountTypeRepository = accountTypeRepository;
            _userRepository = userRepository;
            _accountAdminRepository = accountAdminRepository;
            _accountUserRepository = accountUserRepository;
        }
        public async Task<Result<bool>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var accountType = await _accountTypeRepository.GetAccountTypeByName(request.AccountType);
                var newAccount = new Account
                {
                    Name = request.Name,
                    Zip = request.Zip,
                    Country = request.Country,
                    City = request.City,
                    Street = request.Street,
                    StreetNo = request.StreetNo,
                    ContactEmail = request.ContactEmail,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = request.CreatedBy,
                    AccountTypeId = accountType == null ? 1 : accountType.Id
                };

                await _accountRepository.Create(newAccount);

                return Result<bool>.Ok(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }

        public async Task<Result<bool>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.ModifiedBy);
                var isAdminOfAccount = await _accountAdminRepository.IsAdminOfAccount(user.Id, request.Id);

                if (user.Role.Name != "Admin" && !isAdminOfAccount)
                {
                    return Result<bool>.AccessDenied("No access!");
                }

                var account = await _accountRepository.Get(request.Id);
                if (account == null)
                    return Result<bool>.BadRequest($"Account with Id: {request.Id} not found!");

                if (request.AdminEmails != null)
                    await UpdateAccountAdmins(account.Id, request.ModifiedBy, request.AdminEmails);

                if (request.UserEmails != null)
                    await UpdateAccountUsers(account.Id, request.ModifiedBy, request.UserEmails);

                var accountType = await _accountTypeRepository.GetAccountTypeByName(request.AccountType);

                UpdateAccountProperties(account, request, accountType);

                var res = await _accountRepository.Update(account);
                return Result<bool>.Ok(res);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }

        public async Task<Result<bool>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _accountRepository.Delete(request.Id);

                return Result<bool>.Ok(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure(e.Message);
            }
        }

        private void UpdateAccountProperties(Account currentAccount, UpdateAccountCommand request, AccountType accountType)
        {
            currentAccount.Name = request.Name;
            currentAccount.Zip = request.Zip;
            currentAccount.Country = request.Country;
            currentAccount.City = request.City;
            currentAccount.Street = request.Street;
            currentAccount.StreetNo = request.StreetNo;
            currentAccount.ContactEmail = request.ContactEmail;
            currentAccount.AccountTypeId = accountType == null ? 1 : accountType.Id;
            currentAccount.ModifiedBy = request.ModifiedBy;
            currentAccount.MoidifiedAt = DateTime.UtcNow;
        }

        private async Task UpdateAccountAdmins(int accountId, string modifyingUser, IEnumerable<string> adminEmails)
        {
            var currentAccountAdmins = _accountRepository.Get(accountId).Result.Admins;
            var newAccountAdmins = new List<AccountAdmin>();
            foreach (var adminEmail in adminEmails)
            {
                var user = await _userRepository.GetUserByEmail(adminEmail);
                if (user != null)
                    newAccountAdmins.Add(new AccountAdmin { AccountId = accountId, UserId = user.Id, CreatedBy = modifyingUser, CreatedAt = DateTime.UtcNow });
            }
            await _accountAdminRepository.Update(currentAccountAdmins, newAccountAdmins);
        }

        private async Task UpdateAccountUsers(int accountId, string modifyingUser, IEnumerable<string> userEmails)
        {
            var currentAccountUsers = _accountRepository.Get(accountId).Result.Users;
            var newAccountUsers = new List<AccountUser>();
            foreach (var userEmail in userEmails)
            {
                var user = await _userRepository.GetUserByEmail(userEmail);
                if (user != null)
                    newAccountUsers.Add(new AccountUser { AccountId = accountId, UserId = user.Id, CreatedBy = modifyingUser, CreatedAt = DateTime.UtcNow });
            }
            await _accountUserRepository.Update(currentAccountUsers, newAccountUsers);
        }
    }
}
