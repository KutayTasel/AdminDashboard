using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameDashboardProject.Domain.Base;
using Microsoft.EntityFrameworkCore.Query;

namespace GameDashboardProject.Application.Repositories
{
    public interface IMySqlReadRepository<T> where T : class, IBaseEntity
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
                                   Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                   Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                   bool enableTracking = false);

        Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null,
                                           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                           bool enableTracking = false,
                                           int currentPage = 1, int pageSize = 10);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate,
                         Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                         bool enableTracking = false);

        IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false);

        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

        Task<T> GetByIdAsync(Guid id, bool tracking = true);
    }
}
