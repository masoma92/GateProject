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
    public interface IAccountAdminRepository
    {
        Task<bool> Update(IEnumerable<AccountAdmin> currentAccountAdmins, IEnumerable<AccountAdmin> newAccountAdmins);
        Task<IEnumerable<User>> GetAllUsersByAccountId(int accountId);
    }
    public class AccountAdminRepository : IAccountAdminRepository
    {
        private readonly GPDbContext _context;

        public AccountAdminRepository(GPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersByAccountId(int accountId)
        {
            return await _context.AccountAdmins.Include(x => x.User).Where(x => x.AccountId == accountId).Select(x => x.User).ToListAsync();
        }

        public async Task<bool> Update(IEnumerable<AccountAdmin> currentAccountAdmins, IEnumerable<AccountAdmin> newAccountAdmins)
        {
            _context.Set<AccountAdmin>().RemoveRange(currentAccountAdmins.Except(newAccountAdmins, x => x.UserId));
            _context.Set<AccountAdmin>().AddRange(newAccountAdmins.Except(currentAccountAdmins, x => x.UserId));
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
