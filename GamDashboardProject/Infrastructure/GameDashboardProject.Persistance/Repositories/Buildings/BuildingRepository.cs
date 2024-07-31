using MongoDB.Driver;
using GameDashboardProject.Domain.Buildings;
using System.Threading.Tasks;
using System.Collections.Generic;
using GameDashboardProject.Application.Repositories;
using MongoDB.Bson;

namespace GameDashboardProject.Persistence.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly IMongoCollection<Building> _buildingCollection;

        public BuildingRepository(IMongoClient mongoClient, string databaseName)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _buildingCollection = database.GetCollection<Building>("Buildings");
        }

        public async Task AddAsync(Building building)
        {
            await _buildingCollection.InsertOneAsync(building);
        }

        public async Task<List<Building>> GetAllAsync()
        {
            return await _buildingCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Building> GetByIdAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            return await _buildingCollection.Find(b => b.Id == objectId).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Building building)
        {
            await _buildingCollection.ReplaceOneAsync(b => b.Id == building.Id, building);
        }

        public List<Building> GetBuildingsByTypeIdAsync(string buildingTypeId)
        {
            return _buildingCollection.Find(f => f.BuildingTypeId == buildingTypeId).ToList();
        }
    }
}
