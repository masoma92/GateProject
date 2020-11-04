using GateProjectBackend.BusinessLogic.CommandHandlers.Commands.Dashboard;
using GateProjectBackend.BusinessLogic.CommandHandlers.Responses.Dashboard;
using GateProjectBackend.BusinessLogic.RequestHandlers.Requests.Dashboard;
using GateProjectBackend.BusinessLogic.RequestHandlers.Responses.Dashboard;
using GateProjectBackend.Common;
using GateProjectBackend.Data.Repositories;
using GateProjectBackend.Resources;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.BusinessLogic.CommandHandlers
{
    public class DashboardCommandHandler :
        IRequestHandler<CreateAccountChart, Result<ChartResponse>>,
        IRequestHandler<CreateGateUsageChart, Result<ChartResponse>>,
        IRequestHandler<GetEnters, Result<ListResult<GetEntersResponse>>>

    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogService _logService;
        private readonly IAccountAdminRepository _accountAdminRepository;
        private readonly IAccountUserRepository _accountUserRepository;

        public DashboardCommandHandler(IAccountRepository accountRepository,
            IUserRepository userRepository,
            ILogService logService,
            IAccountAdminRepository accountAdminRepository,
            IAccountUserRepository accountUserRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _logService = logService;
            _accountAdminRepository = accountAdminRepository;
            _accountUserRepository = accountUserRepository;
        }
        public async Task<Result<ChartResponse>> Handle(CreateAccountChart request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.From > request.To)
                    return Result<ChartResponse>.BadRequest("From date cannot be bigger than To");

                var accounts = await _accountRepository.GetAll();

                var data = new Dictionary<string, int>();

                var startDate = request.From;

                while (startDate < request.To)
                {
                    data.Add(startDate.ToString("MM.yyyy"), 0);
                    startDate = startDate.AddMonths(1);
                }

                if (!data.ContainsKey(request.To.ToString("MM.yyyy")))
                    data.Add(request.To.ToString("MM.yyyy"), 0);

                foreach (var account in accounts)
                {
                    var creationDate = account.CreatedAt.ToString("MM.yyyy");
                    if (data.ContainsKey(creationDate)) {
                        data[creationDate]++;
                    }
                }

                var chartResponse = new ChartResponse { ChartData = data };

                return Result<ChartResponse>.Ok(chartResponse);

            }
            catch (Exception e)
            {
                return Result<ChartResponse>.Failure(e.Message);
            }
        }

        public async Task<Result<ChartResponse>> Handle(CreateGateUsageChart request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.From > request.To)
                    return Result<ChartResponse>.BadRequest("From date cannot be bigger than To");

                var user = await _userRepository.GetUserByEmail(request.RequestedBy);

                var logs = await _logService.GetAll();
                logs = logs.Where(x => x.UserId == user.Id && x.EventTypeId == 4);

                var data = new Dictionary<string, int>();

                var startDate = request.From;

                while (startDate < request.To)
                {
                    data.Add(startDate.ToString("dd.MM.yyyy"), 0);
                    startDate = startDate.AddDays(1);
                }

                if (!data.ContainsKey(request.To.ToString("dd.MM.yyyy")))
                    data.Add(request.To.ToString("dd.MM.yyyy"), 0);

                foreach (var log in logs)
                {
                    var creationDate = log.CreatedAt.ToString("dd.MM.yyyy");
                    if (data.ContainsKey(creationDate))
                    {
                        data[creationDate]++;
                    }
                }

                var chartResponse = new ChartResponse { ChartData = data };

                return Result<ChartResponse>.Ok(chartResponse);

            }
            catch (Exception e)
            {
                return Result<ChartResponse>.Failure(e.Message);
            }
        }

        public async Task<Result<ListResult<GetEntersResponse>>> Handle(GetEnters request, CancellationToken cancellationToken)
        {
            try
            {
                if (!IsAdminOrAccountAdmin(request.RequestedEmail, request.AccountId))
                    return Result<ListResult<GetEntersResponse>>.AccessDenied("No access!");

                var result = await _logService.GetAll();

                var myLogs = result.Where(x =>
                x.AccountId == request.AccountId &&
                x.EventTypeId == 4 &&
                x.Action == "success" &&
                x.CreatedAt >= request.From &&
                x.CreatedAt <= request.To);

                var listResponse = new List<GetEntersResponse>();

                foreach (var log in myLogs)
                {
                    var res = listResponse.FirstOrDefault(x => x.Email == log.User.Email && x.GateName == log.Gate.Name && x.FirstUse.Date == log.CreatedAt.Date);
                    if (res == null)
                    {
                        listResponse.Add(new GetEntersResponse
                        {
                            Name = $"{log.User.FirstName} {log.User.LastName}",
                            Email = log.User.Email,
                            FirstUse = log.CreatedAt,
                            Date = log.CreatedAt.Date,
                            GateName = log.Gate.Name,
                            LastUse = log.CreatedAt,
                            IsUserOfAccount = await _accountUserRepository.Get(request.AccountId, log.UserId.Value) == null ? false : true
                        });
                    }
                    else
                    {
                        if (res.FirstUse > log.CreatedAt)
                            res.FirstUse = log.CreatedAt;
                        if (res.LastUse < log.CreatedAt)
                            res.LastUse = log.CreatedAt;
                    }
                }

                if (!String.IsNullOrWhiteSpace(request.Filtering))
                    listResponse = listResponse.Where(x => x.Name.Contains(request.Filtering) || x.Email.Contains(request.Filtering) || x.GateName.Contains(request.Filtering)).ToList();

                var response = new ListResult<GetEntersResponse>(listResponse, listResponse.Count());

                return Result<ListResult<GetEntersResponse>>.Ok(response);
            }
            catch (Exception e)
            {
                return Result<ListResult<GetEntersResponse>>.Failure(e.Message);
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
