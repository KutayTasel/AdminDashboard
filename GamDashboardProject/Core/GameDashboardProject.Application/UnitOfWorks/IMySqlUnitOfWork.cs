using System;
using System.Threading.Tasks;
using GameDashboardProject.Application.Repositories;
using GameDashboardProject.Domain.Base;

namespace GameDashboardProject.Application.UnitOfWorks
{
    public interface IMySqlUnitOfWork : IAsyncDisposable
    {
        IMySqlReadRepository<T> GetReadRepository<T>() where T : class, IBaseEntity;
        IMySqlWriteRepository<T> GetWriteRepository<T>() where T : class, IBaseEntity;
        Task<int> SaveAsync();
        int Save();
    }
}
