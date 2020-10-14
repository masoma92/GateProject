using GateProjectBackend.Common;
using GateProjectBackend.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Data.Repositories
{
    public interface IGateTypeRepository
    {
        Task<GateType> GetGateTypeByName(string name);
        Task<ListResult<GateType>> GetList();
    }
    public class GateTypeRepository : IGateTypeRepository
    {
        private readonly GPDbContext _context;

        public GateTypeRepository(GPDbContext context)
        {
            _context = context;
        }
        public async Task<GateType> GetGateTypeByName(string name)
        {
            return await _context.GateTypes.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<ListResult<GateType>> GetList()
        {
            var query = await _context.GateTypes.ToListAsync();

            return new ListResult<GateType>(query, query.Count);
        }
    }
}
