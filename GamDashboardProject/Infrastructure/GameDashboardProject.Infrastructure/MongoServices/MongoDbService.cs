// MongoDbService.cs
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace GameDashboardProject.Infrastructure.MongoServices
{
    public class MongoDbService : IMongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoConnection:ConnectionString");
            var mongoClient = new MongoClient(connectionString);
            _database = mongoClient.GetDatabase(configuration.GetConnectionString("MongoConnection:DatabaseName"));
        }

        public IMongoDatabase Database => _database;
    }
}
