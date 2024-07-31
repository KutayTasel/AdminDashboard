using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameDashboardProject.Application.Repositories;
using GameDashboardProject.Domain.Base;
using GameDashboardProject.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace GameDashboardProject.Persistence.Repositories
{
    public class MySqlReadRepository<T> : IMySqlReadRepository<T> where T : class, IBaseEntity
    {
        private readonly GameDashboardDbContext _dbContext;

        public MySqlReadRepository(GameDashboardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<T> Table => _dbContext.Set<T>();

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
                                                Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                                Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                                bool enableTracking = false)
        {
            IQueryable<T> queryable = Table.AsQueryable();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null)
                return await orderBy(queryable).ToListAsync();

            return await queryable.ToListAsync();
        }

        public async Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null,
                                                        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                                        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                                        bool enableTracking = false,
                                                        int currentPage = 1, int pageSize = 10)
        {
            IQueryable<T> queryable = Table.AsQueryable();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);
            if (predicate != null) queryable = queryable.Where(predicate);
            if (orderBy != null)
                return await orderBy(queryable).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

            return await queryable.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate,
                                      Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
                                      bool enableTracking = false)
        {
            IQueryable<T> queryable = Table.AsQueryable();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            if (include != null) queryable = include(queryable);

            return await queryable.FirstOrDefaultAsync(predicate);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
        {
            IQueryable<T> queryable = Table.AsQueryable();
            if (!enableTracking) queryable = queryable.AsNoTracking();
            return queryable.Where(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            IQueryable<T> queryable = Table.AsNoTracking();
            if (predicate != null) queryable = queryable.Where(predicate);
            return await queryable.CountAsync();
        }

        public async Task<T> GetByIdAsync(Guid id, bool tracking = true)
        {
            IQueryable<T> query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data => data.Id == id);
        }
    }
}
