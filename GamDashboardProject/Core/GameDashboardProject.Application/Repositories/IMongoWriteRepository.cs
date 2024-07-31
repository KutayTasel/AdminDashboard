using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameDashboardProject.Domain.Base;

namespace GameDashboardProject.Application.Repositories
{
    public interface IMongoWriteRepository<T> where T : class, IMongoEntity, new()
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IList<T> entities);
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(string id); 
        Task<T> FindAsync(Expression<Func<T, bool>> filter);
    }
}
