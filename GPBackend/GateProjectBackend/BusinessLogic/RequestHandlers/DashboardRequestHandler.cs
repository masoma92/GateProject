using GateProjectBackend.BusinessLogic.RequestHandlers.Requests;
using GateProjectBackend.BusinessLogic.RequestHandlers.Requests.Dashboard;
using GateProjectBackend.Common;
using GateProjectBackend.Data.Repositories;
using GateProjectBackend.Resources;
using MediatR;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.RequestHandlers
{
    public class DashboardRequestHandler :
        IRequestHandler<SumAccounts, Result<int>>,
        IRequestHandler<SumGates, Result<int>>,
        IRequestHandler<SumUsers, Result<int>>,
        IRequestHandler<SumErrors, Result<int>>,
        IRequestHandler<SumGatesByAccount, Result<int>>,
        IRequestHandler<SumUsersByAccount, Result<int>>,
        IRequestHandler<SumAdminsByAccount, Result<int>>,
        IRequestHandler<SumErrorsByAccount, Result<int>>,
        IRequestHandler<SumGateAccesses, Result<int>>,
        IRequestHandler<SumGateAdminAccesses, Result<int>>,
        IRequestHandler<GetRegDate, Result<DateTime>>,
        IRequestHandler<GetLastGateAccessDate, Result<DateTime>>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IGateRepository _gateRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAccountAdminRepository _accountAdminRepository;
        private readonly ILogService _logService;

        public DashboardRequestHandler(
            IAccountRepository accountRepository,
            IGateRepository gateRepository,
            IUserRepository userRepository,
            IAccountAdminRepository accountAdminRepository,
            ILogService logService)
        {
            _accountRepository = accountRepository;
            _gateRepository = gateRepository;
            _userRepository = userRepository;
            _accountAdminRepository = accountAdminRepository;
            _logService = logService;
        }

        public async Task<Result<int>> Handle(SumAccounts request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _accountRepository.GetSumOfAccounts();

                return Result<int>.Ok(result);
            }
            catch (Exception e)
            {
                return Result<int>.Failure(e.Message);
            }
        }

        public async Task<Result<int>> Handle(SumGates request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _gateRepository.GetSumOfGates();

                return Result<int>.Ok(result);
            }
            catch (Exception e)
            {
                return Result<int>.Failure(e.Message);
            }
        }

        public async Task<Result<int>> Handle(SumUsers request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userRepository.GetSumOfUsers();

                return Result<int>.Ok(result);
            }
            catch (Exception e)
            {
                return Result<int>.Failure(e.Message);
            }
        }

        public async Task<Result<int>> Handle(SumErrors request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _logService.GetAll();

                var errorNum = result.Where(x => x.EventType.Name == "Error").Count();

                return Result<int>.Ok(errorNum);
            }
            catch (Exception e)
            {
                return Result<int>.Failure(e.Message);
            }
        }

        public async Task<Result<int>> Handle(SumGatesByAccount request, CancellationToken cancellationToken)
        {
            try
            {
                if (!IsAdminOrAccountAdmin(request.RequestedEmail, request.AccountId))
                    return Result<int>.AccessDenied("No access!");

                var result = await _gateRepository.GetSumOfGatesByAccountId(request.AccountId);

                return Result<int>.Ok(result);
            }
            catch (Exception e)
            {
                return Result<int>.Failure(e.Message);
            }
        }

        public async Task<Result<int>> Handle(SumUsersByAccount request, CancellationToken cancellationToken)
        {
            try
            {
                if (!IsAdminOrAccountAdmin(request.RequestedEmail, request.AccountId))
                    return Result<int>.AccessDenied("No access!");

                var result = await _userRepository.GetSumOfUsersByAccountId(request.AccountId);

                return Result<int>.Ok(result);
            }
            catch (Exception e)
            {
                return Result<int>.Failure(e.Message);
            }
        }

        public async Task<Result<int>> Handle(SumAdminsByAccount request, CancellationToken cancellationToken)
        {
            try
            {
                if (!IsAdminOrAccountAdmin(request.RequestedEmail, request.AccountId))
                    return Result<int>.AccessDenied("No access!");

                var result = await _userRepository.GetSumOfAdminsByAccountId(request.AccountId);

                return Result<int>.Ok(result);
            }
            catch (Exception e)
            {
                return Result<int>.Failure(e.Message);
            }
        }

        public async Task<Result<int>> Handle(SumErrorsByAccount request, CancellationToken cancellationToken)
        {
            try
            {
                if (!IsAdminOrAccountAdmin(request.RequestedEmail, request.AccountId))
                    return Result<int>.AccessDenied("No access!");

                var result = await _logService.GetAll();

                var errorNum = result.Where(x => x.EventType.Name == "Error" && x.AccountId == request.AccountId).Count();

                return Result<int>.Ok(errorNum);
            }
            catch (Exception e)
            {
                return Result<int>.Failure(e.Message);
            }
        }

        public async Task<Result<int>> Handle(SumGateAccesses request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.RequestedEmail);

                var result = await _gateRepository.GetSumOfAccesses(user.Id);

                return Result<int>.Ok(result);
            }
            catch (Exception e)
            {
                return Result<int>.Failure(e.Message);
            }
        }

        public async Task<Result<int>> Handle(SumGateAdminAccesses request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.RequestedEmail);

                var result = await _gateRepository.GetSumOfAdminAccesses(user.Id);

                return Result<int>.Ok(result);
            }
            catch (Exception e)
            {
                return Result<int>.Failure(e.Message);
            }
        }

        public async Task<Result<DateTime>> Handle(GetRegDate request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.RequestedEmail);

                return Result<DateTime>.Ok(user.CreatedAt);
            }
            catch (Exception e)
            {
                return Result<DateTime>.Failure(e.Message);
            }
        }

        public async Task<Result<DateTime>> Handle(GetLastGateAccessDate request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByEmail(request.RequestedEmail);
                var result = await _logService.GetAll();

                var errorNum = result.Where(x => x.EventType.Name == "Enter" && x.UserId == user.Id).OrderBy(x => x.CreatedAt).FirstOrDefault();

                if (errorNum == null)
                    return Result<DateTime>.NotFound("There isn't eny enter log for this user");

                return Result<DateTime>.Ok(errorNum.CreatedAt);
            }
            catch (Exception e)
            {
                return Result<DateTime>.Failure(e.Message);
            }
        }

        private bool IsAdminOrAccountAdmin(string email, int accountId)
        {
            var user = _userRepository.GetUserByEmail(email).Result;
            var isAdminOfAccount = _accountAdminRepository.IsAdminOfAccount(user.Id, accountId).Result;

            if (user.Role.Name != "Admin" && !isAdminOfAccount)
            {
                return false;
            }

            return true;
        }
    }
}
