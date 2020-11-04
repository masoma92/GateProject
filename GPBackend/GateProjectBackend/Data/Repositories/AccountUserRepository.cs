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
    public interface IAccountUserRepository
    {
        Task<bool> Update(IEnumerable<AccountUser> currentAccountUsers, IEnumerable<AccountUser> newAccountUsers);
        Task<IEnumerable<User>> GetAllUsersByAccountId(int accountId);
        Task<AccountUser> Get(int accountId, int userId);
    }
    public class AccountUserRepository : IAccountUserRepository
    {
        private readonly GPDbContext _context;

        public AccountUserRepository(GPDbContext context)
        {
            _context = context;
        }

        public async Task<AccountUser> Get(int accountId, int userId)
        {
            return await _context.AccountUsers.FirstOrDefaultAsync(x => x.UserId == userId && x.AccountId == accountId);
        }

        public async Task<IEnumerable<User>> GetAllUsersByAccountId(int accountId)
        {
            return await _context.AccountUsers.Include(x => x.User).Where(x => x.AccountId == accountId).Select(x => x.User).ToListAsync();
        }

        public async Task<bool> Update(IEnumerable<AccountUser> currentAccountUsers, IEnumerable<AccountUser> newAccountUsers)
        {
            _context.Set<AccountUser>().RemoveRange(currentAccountUsers.Except(newAccountUsers, x => x.UserId));
            _context.Set<AccountUser>().AddRange(newAccountUsers.Except(currentAccountUsers, x => x.UserId));
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
