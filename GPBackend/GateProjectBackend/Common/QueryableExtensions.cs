using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GateProjectBackend.Common
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> ApplySorting<TEntity, TResult>(this IQueryable<TEntity> query, Sorting sorting, string columnName, Expression<Func<TEntity, TResult>> prop)
        {
            if (sorting.SortBy != columnName)
                return query;

            return sorting.IsSortAscending ? query.OrderBy(prop) : query.OrderByDescending(prop);
        }

        public static IQueryable<TEntity> ApplyPaging<TEntity>(this IQueryable<TEntity> query, PaginationEntry pagination)
        {
            var startIndex = (pagination.CurrentPage - 1) * pagination.PageSize;
            return query.Skip(startIndex).Take(pagination.PageSize);
        }
    }
}
