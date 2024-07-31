using GameDashboardProject.Application.Repositories;
using GameDashboardProject.Application.UnitOfWorks;
using GameDashboardProject.Domain.Base;
using GameDashboardProject.Domain.Buildings; 
using GameDashboardProject.Infrastructure.MongoServices;
using GameDashboardProject.Persistence.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameDashboardProject.Persistence.UnitOfWorks
{
    public class MongoUnitOfWork : IMongoUnitOfWorkk
    {
        private readonly IMongoDbService _mongoDbService;

        public MongoUnitOfWork(IMongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
            Console.WriteLine("MongoUnitOfWork initialized.");
        }

        public IMongoReadRepository<T> GetReadRepository<T>() where T : class, IMongoEntity, new()
        {
            Console.WriteLine($"Creating read repository for type: {typeof(T).Name}");
            return new MongoReadRepository<T>(_mongoDbService);
        }

        public IMongoWriteRepository<T> GetWriteRepository<T>() where T : class, IMongoEntity, new()
        {
            Console.WriteLine($"Creating write repository for type: {typeof(T).Name}");
            return new MongoWriteRepository<T>(_mongoDbService);
        }

        public async Task<IList<T>> GetAllAsync<T>() where T : class, IMongoEntity, new()
        {
            var repository = GetReadRepository<T>();
            return await repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync<T>(string id) where T : class, IMongoEntity, new()
        {
            var repository = GetReadRepository<T>();
            return await repository.GetByIdAsync(id);
        }

        // Yeni eklenen GetAllBuildingTypesAsync metodu
        public async Task<IList<BuildingType>> GetAllBuildingTypesAsync()
        {
            var repository = GetReadRepository<BuildingType>();
            return await repository.GetAllBuildingTypesAsync();
        }

        // Implementation of IsBuildingTypeAlreadyAdded method
        public async Task<bool> IsBuildingTypeAlreadyAdded(ObjectId buildingTypeId)
        {
            var repository = GetReadRepository<Building>();
            var existingBuilding = await repository.FindAsync(b => b.BuildingTypeId == buildingTypeId.ToString());
            return existingBuilding != null;
        }

        public Task<int> SaveAsync()
        {
            Console.WriteLine("SaveAsync called. MongoDB commits changes immediately, so this method is generally unnecessary.");
            return Task.FromResult(0);
        }

        public async ValueTask DisposeAsync()
        {
            Console.WriteLine("DisposeAsync called.");
            await Task.CompletedTask;
        }
    }
}
