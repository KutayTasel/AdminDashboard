using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameDashboardProject.Domain.Base;
using GameDashboardProject.Domain.Buildings;
using MongoDB.Driver.Linq;

namespace GameDashboardProject.Application.Repositories
{
    public interface IMongoReadRepository<T> where T : IMongoEntity
    {
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
                                   Func<IMongoQueryable<T>, IOrderedMongoQueryable<T>>? orderBy = null);

        Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null,
                                           Func<IMongoQueryable<T>, IOrderedMongoQueryable<T>>? orderBy = null,
                                           int currentPage = 1, int pageSize = 10);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate);

        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

        Task<T> GetByIdAsync(string id);
        Task<IList<BuildingType>> GetAllBuildingTypesAsync();
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
    }
}
