using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GameDashboardProject.Application.Repositories;
using GameDashboardProject.Domain.Base;
using GameDashboardProject.Infrastructure.MongoServices;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GameDashboardProject.Persistence.Repositories
{
    public class MongoWriteRepository<T> : IMongoWriteRepository<T> where T : class, IMongoEntity, new()
    {
        private readonly IMongoCollection<T> _collection;

        public MongoWriteRepository(IMongoDbService mongoDbService)
        {
            _collection = mongoDbService.Database.GetCollection<T>(typeof(T).Name);
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }
        public async Task<T> FindAsync(Expression<Func<T, bool>> filter)
        {
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task AddRangeAsync(IList<T> entities)
        {
            await _collection.InsertManyAsync(entities);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            var result = await _collection.ReplaceOneAsync(filter, entity);

            if (result.MatchedCount == 0)
                throw new KeyNotFoundException("The entity to update was not found.");

            return entity;
        }

        public async Task RemoveAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, ObjectId.Parse(id));
            var result = await _collection.DeleteOneAsync(filter);
            if (result.DeletedCount == 0)
                throw new KeyNotFoundException("The entity to delete was not found.");
        }
    }
}
