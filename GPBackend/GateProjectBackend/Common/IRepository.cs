using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Common
{
    public interface IRepository<T> where T : class
    {
        Task<ListResult<T>> GetList(PaginationEntry pagination, Sorting sorting, string filtering);

        Task<T> Get(int id);

        Task<bool> Create(T entity);

        Task<bool> Update(T entity);

        Task<bool> Delete(int id);
    }
}
