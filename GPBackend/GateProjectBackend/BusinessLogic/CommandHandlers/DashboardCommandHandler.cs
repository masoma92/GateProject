using GateProjectBackend.BusinessLogic.CommandHandlers.Commands.Dashboard;
using GateProjectBackend.BusinessLogic.CommandHandlers.Responses.Dashboard;
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
        IRequestHandler<CreateGateUsageChart, Result<ChartResponse>>

    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogService _logService;

        public DashboardCommandHandler(IAccountRepository accountRepository,
            IUserRepository userRepository,
            ILogService logService)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _logService = logService;
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
    }
}
