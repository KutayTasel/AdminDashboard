using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GameDashboardProject.Domain.Base;
using GameDashboardProject.Application.Repositories;
using GameDashboardProject.Persistence.Context;

namespace GameDashboardProject.Persistence.Repositories
{
    public class MySqlWriteRepository<T> : IMySqlWriteRepository<T> where T : class, IBaseEntity
    {
        private readonly GameDashboardDbContext _dbContext;

        public MySqlWriteRepository(GameDashboardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private DbSet<T> Table => _dbContext.Set<T>();

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IList<T> entities)
        {
            await Table.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            Table.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task RemoveAsync(T entity)
        {
            Table.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await Table.FindAsync(id);
            if (entity != null)
            {
                Table.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
