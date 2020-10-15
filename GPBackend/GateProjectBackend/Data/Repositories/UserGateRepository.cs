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
    public interface IUserGateRepository
    {
        Task<bool> Update(IEnumerable<UserGate> currentGateUsers, IEnumerable<UserGate> newGateUsers);
        Task<IEnumerable<UserGate>> GetAllUsersByGateId(int gateId);
        Task<IEnumerable<UserGate>> GetAllGatesByUserIdAndAccess(int userId);
        Task<bool> CheckAccess(int gateId, int userId);
        Task<bool> CheckAdminAccess(int gateId, int userId);
    }
    public class UserGateRepository : IUserGateRepository
    {
        private readonly GPDbContext _context;

        public UserGateRepository(GPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckAccess(int gateId, int userId)
        {
            return await _context.UserGates.AnyAsync(x => x.UserId == userId && x.GateId == gateId && x.AccessRight);
        }

        public async Task<bool> CheckAdminAccess(int gateId, int userId)
        {
            return await _context.UserGates.AnyAsync(x => x.UserId == userId && x.GateId == gateId && x.AccessRight && x.AdminRight);
        }

        public async Task<IEnumerable<UserGate>> GetAllGatesByUserIdAndAccess(int userId)
        {
            return await _context.UserGates.Include(x => x.Gate).Where(x => x.UserId == userId && x.AccessRight == true).ToListAsync();
        }

        public async Task<IEnumerable<UserGate>> GetAllUsersByGateId(int gateId)
        {
            return await _context.UserGates.Include(x => x.User).Where(x => x.GateId == gateId).ToListAsync();
        }

        public async Task<bool> Update(IEnumerable<UserGate> currentGateUsers, IEnumerable<UserGate> newGateUsers)
        {
            _context.Set<UserGate>().RemoveRange(currentGateUsers.Except(newGateUsers, x => x.UserId));
            _context.Set<UserGate>().AddRange(newGateUsers.Except(currentGateUsers, x => x.UserId));
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
