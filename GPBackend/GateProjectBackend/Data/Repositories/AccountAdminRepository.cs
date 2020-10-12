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
        Task<bool> Update(int accountId, IEnumerable<AccountAdmin> newAccountAdmins);
    }
    public class AccountAdminRepository : IAccountAdminRepository
    {
        private readonly GPDbContext _context;

        public AccountAdminRepository(GPDbContext context)
        {
            _context = context;
        }

        // modositani
        public async Task<bool> Update(int accountId, IEnumerable<AccountAdmin> newAccountAdmins)
        {
            var accountAdmins = _context.AccountAdmins.ToList();
            accountAdmins.RemoveAll(x => x.AccountId == accountId);
            foreach (var item in newAccountAdmins)
            {
                _context.AccountAdmins.Add(item);
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
