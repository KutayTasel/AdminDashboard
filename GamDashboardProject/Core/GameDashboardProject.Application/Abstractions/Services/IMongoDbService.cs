using MongoDB.Driver;

namespace GameDashboardProject.Infrastructure.MongoServices
{
    public interface IMongoDbService
    {
        IMongoDatabase Database { get; }
    }
}
