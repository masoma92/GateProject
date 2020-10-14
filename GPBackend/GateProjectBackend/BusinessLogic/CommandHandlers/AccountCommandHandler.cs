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

        public AccountCommandHandler(
            IAccountRepository accountRepository,
            IAccountTypeRepository accountTypeRepository,
            IUserRepository userRepository,
            IAccountAdminRepository accountAdminRepository)
        {
            _accountRepository = accountRepository;
            _accountTypeRepository = accountTypeRepository;
            _userRepository = userRepository;
            _accountAdminRepository = accountAdminRepository;
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
                var account = await _accountRepository.Get(request.Id);
                if (account == null)
                    return Result<bool>.BadRequest($"Account with Id: {request.Id} not found!");

                if (request.AdminEmails != null)
                    await UpdateAccountAdmins(account.Id, request.ModifiedBy, request.AdminEmails);

                UpdateAccountProperties(account, request);

                var res = await _accountRepository.Update( account);
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

        private void UpdateAccountProperties(Account currentAccount, UpdateAccountCommand request)
        {
            currentAccount.Name = request.Name;
            currentAccount.Zip = request.Zip;
            currentAccount.Country = request.Country;
            currentAccount.City = request.City;
            currentAccount.Street = request.Street;
            currentAccount.StreetNo = request.StreetNo;
            currentAccount.ContactEmail = request.ContactEmail;
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
    }
}
