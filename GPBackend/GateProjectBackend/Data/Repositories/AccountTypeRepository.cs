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
    public interface IAccountTypeRepository
    {
        Task<AccountType> GetAccountTypeByName(string name);
        Task<ListResult<AccountType>> GetList();
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

        public async Task<ListResult<AccountType>> GetList()
        {
            var query = await _context.AccountTypes.ToListAsync();

            return new ListResult<AccountType>(query, query.Count);
        }
    }
}
