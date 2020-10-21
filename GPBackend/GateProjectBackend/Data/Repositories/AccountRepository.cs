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
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAccountByName(string name);
    }
    public class AccountRepository : IAccountRepository
    {
        private readonly GPDbContext _context;

        public AccountRepository(GPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(Account entity)
        {
            _context.Accounts.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var account = await _context.Accounts.Include(x => x.Gates).FirstOrDefaultAsync(x => x.Id == id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Account> Get(int id)
        {
            return await _context.Accounts.Include(x => x.Admins).Include(x => x.Users).Include(x => x.AccountType).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Account> GetAccountByName(string name)
        {
            return await _context.Accounts.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<ListResult<Account>> GetList(PaginationEntry pagination, Sorting sorting, string filtering)
        {
            var query = _context.Accounts.Include(x => x.AccountType).Include(x => x.Admins).Include(x => x.Users).OrderBy(x => x.Id).AsQueryable();

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

            return new ListResult<Account>(result, result.Count);
        }

        public async Task<bool> Update(Account entity)
        {
            _context.Accounts.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
