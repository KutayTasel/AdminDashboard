using MongoDB.Driver;
using GameDashboardProject.Domain.Buildings;
using System.Threading.Tasks;
using System.Collections.Generic;
using GameDashboardProject.Application.Repositories;
using MongoDB.Bson;

namespace GameDashboardProject.Persistence.Repositories
{
    public class BuildingTypeRepository : IBuildingTypeRepository
    {
        private readonly IMongoCollection<BuildingType> _buildingTypeCollection;

        public BuildingTypeRepository(IMongoClient mongoClient, string databaseName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _buildingTypeCollection = database.GetCollection<BuildingType>("BuildingTypes");
        }

        public async Task AddAsync(BuildingType buildingType)
        {
            await _buildingTypeCollection.InsertOneAsync(buildingType);
        }

        public async Task<List<BuildingType>> GetAllAsync()
        {
            return await _buildingTypeCollection.Find(_ => true).ToListAsync();
        }

        public async Task<BuildingType> GetByIdAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            return await _buildingTypeCollection.Find(bt => bt.Id == objectId).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(BuildingType buildingType)
        {
            await _buildingTypeCollection.ReplaceOneAsync(bt => bt.Id == buildingType.Id, buildingType);
        }
    }
}
