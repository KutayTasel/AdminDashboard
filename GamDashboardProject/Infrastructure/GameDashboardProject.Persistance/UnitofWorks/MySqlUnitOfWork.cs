using System;
using System.Threading.Tasks;
using GameDashboardProject.Application.Repositories;
using GameDashboardProject.Application.UnitOfWorks;
using GameDashboardProject.Domain.Base;
using GameDashboardProject.Persistence.Context;
using GameDashboardProject.Persistence.Repositories;

namespace GameDashboardProject.Persistence.UnitOfWorks
{
    public class MySqlUnitOfWork : IMySqlUnitOfWork
    {
        private readonly GameDashboardDbContext _dbContext;

        public MySqlUnitOfWork(GameDashboardDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IMySqlReadRepository<T> GetReadRepository<T>() where T : class, IBaseEntity
        {
            return new MySqlReadRepository<T>(_dbContext);
        }

        public IMySqlWriteRepository<T> GetWriteRepository<T>() where T : class, IBaseEntity
        {
            return new MySqlWriteRepository<T>(_dbContext);
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
