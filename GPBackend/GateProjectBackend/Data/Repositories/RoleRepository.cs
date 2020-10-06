using GateProjectBackend.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Data.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleByUserEmail(string email);
    }
    public class RoleRepository : IRoleRepository
    {
        private readonly GPDbContext _context;

        public RoleRepository(GPDbContext context)
        {
            _context = context;
        }
        public async Task<Role> GetRoleByUserEmail(string email)
        {
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
            return user.Role;
        }
    }
}
