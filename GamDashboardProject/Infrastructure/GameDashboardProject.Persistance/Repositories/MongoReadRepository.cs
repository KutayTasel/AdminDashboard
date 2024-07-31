using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameDashboardProject.Application.Repositories;
using GameDashboardProject.Domain.Base;
using GameDashboardProject.Infrastructure.MongoServices;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using GameDashboardProject.Domain.Buildings;

namespace GameDashboardProject.Persistence.Repositories
{
    public class MongoReadRepository<T> : IMongoReadRepository<T> where T : class, IMongoEntity, new()
    {
        private readonly IMongoCollection<T> _collection;

        public MongoReadRepository(IMongoDbService mongoDbService)
        {
            _collection = mongoDbService.Database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
                                                Func<IMongoQueryable<T>, IOrderedMongoQueryable<T>>? orderBy = null)
        {
            IMongoQueryable<T> query = _collection.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null,
                                                        Func<IMongoQueryable<T>, IOrderedMongoQueryable<T>>? orderBy = null,
                                                        int currentPage = 1, int pageSize = 10)
        {
            IMongoQueryable<T> query = _collection.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            return await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            IMongoQueryable<T> query = _collection.AsQueryable();
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            IMongoQueryable<T> query = _collection.AsQueryable();
            if (predicate != null)
                query = query.Where(predicate);
            return await query.CountAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var objectId = ObjectId.Parse(id); 
            return await _collection.Find(e => e.Id == objectId).FirstOrDefaultAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).FirstOrDefaultAsync();
        }
        public async Task<IList<BuildingType>> GetAllBuildingTypesAsync()
        {
            var buildingTypeCollection = _collection.Database.GetCollection<BuildingType>("BuildingTypes");
            return await buildingTypeCollection.Find(Builders<BuildingType>.Filter.Empty).ToListAsync();
        }
    }
}
