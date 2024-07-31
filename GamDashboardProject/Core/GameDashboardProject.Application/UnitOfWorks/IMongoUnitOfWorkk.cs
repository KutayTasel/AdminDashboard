using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameDashboardProject.Application.Repositories;
using GameDashboardProject.Domain.Base;
using GameDashboardProject.Domain.Buildings;
using MongoDB.Bson;

namespace GameDashboardProject.Application.UnitOfWorks
{
    public interface IMongoUnitOfWorkk : IAsyncDisposable
    {
        IMongoReadRepository<T> GetReadRepository<T>() where T : class, IMongoEntity, new();
        IMongoWriteRepository<T> GetWriteRepository<T>() where T : class, IMongoEntity, new();
        Task<int> SaveAsync();
        Task<IList<T>> GetAllAsync<T>() where T : class, IMongoEntity, new();
        Task<T> GetByIdAsync<T>(string id) where T : class, IMongoEntity, new();
        Task<IList<BuildingType>> GetAllBuildingTypesAsync(); 
        Task<bool> IsBuildingTypeAlreadyAdded(ObjectId buildingTypeId); 
    }
}
