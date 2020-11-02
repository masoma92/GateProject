using GateProjectBackend.Common;
using GateProjectBackend.Data;
using GateProjectBackend.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GateProjectBackend.Resources
{
    public enum EventTypes { User = 1, Error = 2, Info = 3, Enter = 4 }
    public interface ILogService
    {
        Task<bool> Create(string action, EventTypes type, int? userId, int? accountId, int? gateId);
        Task<ListResult<Log>> GetList(PaginationEntry pagination, Sorting sorting, string filtering);
        Task<IEnumerable<Log>> GetAll();

    }
    public class LogService : ILogService
    {
        private readonly GPDbContext _context;

        public LogService(GPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(string action, EventTypes type, int? userId, int? accountId, int? gateId)
        {
            var newLog = new Log
            {
                Action = action,
                EventTypeId = (int)type,
                AccountId = accountId,
                UserId = userId,
                GateId = gateId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "SYSTEM"
            };

            await _context.Logs.AddAsync(newLog);
            _context.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<Log>> GetAll()
        {
            return await _context.Logs.Include(x => x.EventType).Include(x => x.Gate).Include(x => x.User).Include(x => x.Account).ToListAsync();
        }

        public async Task<ListResult<Log>> GetList(PaginationEntry pagination, Sorting sorting, string filtering)
        {
            var query = _context.Logs.Include(x => x.EventType).Include(x => x.Gate).Include(x => x.User).Include(x => x.Account).OrderBy(x => x.Id).AsQueryable();

            // filtering
            if (!string.IsNullOrEmpty(filtering))
            {
                query = query.Where(x => x.Action.Contains(filtering) || x.EventType.Name.Contains(filtering));
            }

            // sorting
            query = query.ApplySorting(sorting, "Id", x => x.Id);
            query = query.ApplySorting(sorting, "name", x => x.EventType.Name);

            // Paging
            var pagedQuery = query.ApplyPaging(pagination);

            var result = await pagedQuery.ToListAsync();

            return new ListResult<Log>(result, result.Count);
        }
    }
}
