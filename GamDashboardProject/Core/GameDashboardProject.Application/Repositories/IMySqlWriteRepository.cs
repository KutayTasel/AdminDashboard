using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameDashboardProject.Domain.Base;

namespace GameDashboardProject.Application.Repositories
{
    public interface IMySqlWriteRepository<T> where T : class, IBaseEntity
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IList<T> entities);
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
