using GateProjectBackend.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace GateProjectBackend.Data.Repositories
{
    public interface IAccountTypeRepository
    {
        Task<AccountType> GetAccountTypeByName(string name);
    }
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly GPDbContext _context;

        public AccountTypeRepository(GPDbContext context)
        {
            _context = context;
        }

        public async Task<AccountType> GetAccountTypeByName(string name)
        {
            return await _context.AccountTypes.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
