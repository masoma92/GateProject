using GateProjectBackend.Common;
using GateProjectBackend.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GateProjectBackend.Data.Repositories
{
    public interface IGateRepository : IRepository<Gate>
    {
        IEnumerable<Gate> GetAllGatesFromAccounts(IEnumerable<Account> accounts);
        bool IsAccountAdminOfTheGate(int gateId, int userId);
    }
    public class GateRepository : IGateRepository
    {
        private readonly GPDbContext _context;

        public GateRepository(GPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(Gate entity)
        {
            _context.Gates.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var gate = await _context.Gates.FirstOrDefaultAsync(x => x.Id == id);
            _context.Gates.Remove(gate);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Gate> Get(int id)
        {
            return await _context.Gates.Include(x => x.Users).Include(x => x.GateType).Include(x => x.Account).FirstOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<Gate> GetAllGatesFromAccounts(IEnumerable<Account> accounts)
        {
            var gates = new List<Gate>();
            foreach (var account in accounts)
            {
                var currentGates = _context.Gates.Where(x => x.AccountId == account.Id);
                foreach (var gate in currentGates)
                {
                    gates.Add(gate);
                }
            }
            return gates;
        }

        public async Task<ListResult<Gate>> GetList(PaginationEntry pagination, Sorting sorting, string filtering)
        {
            var query = _context.Gates.Include(x => x.GateType).Include(x => x.Account).Include(x => x.Users).OrderBy(x => x.Id).AsQueryable();

            // filtering
            if (!string.IsNullOrEmpty(filtering))
            {
                query = query.Where(x => x.Name.Contains(filtering));
            }

            // sorting
            query = query.ApplySorting(sorting, "Id", x => x.Id);
            query = query.ApplySorting(sorting, "name", x => x.Name);

            // Paging
            var pagedQuery = query.ApplyPaging(pagination);

            var result = await pagedQuery.ToListAsync();

            return new ListResult<Gate>(result, result.Count);
        }

        public bool IsAccountAdminOfTheGate(int gateId, int userId)
        {
            var gate = _context.Gates.Include(x => x.Account).FirstOrDefault(x => x.Id == gateId);

            var account = _context.Accounts.Include(x => x.Admins).FirstOrDefault(x => x.Id == gate.Account.Id);

            bool isAdmin = _context.AccountAdmins.Any(x => x.AccountId == account.Id && x.UserId == userId);

            return isAdmin;
        }

        public async Task<bool> Update(Gate entity)
        {
            _context.Gates.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
