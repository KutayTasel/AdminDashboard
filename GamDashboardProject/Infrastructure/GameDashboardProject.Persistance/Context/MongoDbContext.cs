using GameDashboardProject.Domain.Buildings;
using GameDashboardProject.Infrastructure.MongoServices;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoDbService mongoDbService)
    {
        _database = mongoDbService.Database;
    }

    public IMongoCollection<BuildingType> BuildingTypes => _database.GetCollection<BuildingType>("BuildingTypes");
    public IMongoCollection<Building> Buildings => _database.GetCollection<Building>("Buildings");

    public async Task AddOrUpdateSeedData()
    {
        var buildingTypes = new[]
        {
        new BuildingType
        {
            Id = new ObjectId("66a7f898a27741885b97d4f2"),
            Name = "Farm",
            IsOpen = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new BuildingType
        {
            Id = new ObjectId("66a7f898a27741885b97d4f3"),
            Name = "Academy",
            IsOpen = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new BuildingType
        {
            Id = new ObjectId("66a7f898a27741885b97d4f4"),
            Name = "Headquarters",
            IsOpen = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new BuildingType
        {
            Id = new ObjectId("66a7f898a27741885b97d4f5"),
            Name = "LumberMill",
            IsOpen = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        },
        new BuildingType
        {
            Id = new ObjectId("66a7f898a27741885b97d4f6"),
            Name = "Barracks",
            IsOpen = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }
    };

        foreach (var buildingType in buildingTypes)
        {
            var filter = Builders<BuildingType>.Filter.Eq(bt => bt.Id, buildingType.Id);
            var existingType = await BuildingTypes.Find(filter).FirstOrDefaultAsync();

            if (existingType == null)
            {
                try
                {
                    await BuildingTypes.InsertOneAsync(buildingType);
                    Console.WriteLine($"Building type '{buildingType.Name}' added successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error adding building type '{buildingType.Name}': {ex.Message}");
                }
            }
            else
            {
                // Update the existing document if necessary
                var update = Builders<BuildingType>.Update
                    .Set(bt => bt.Name, buildingType.Name)
                    .Set(bt => bt.IsOpen, buildingType.IsOpen)
                    .Set(bt => bt.UpdatedAt, DateTime.UtcNow);

                try
                {
                    await BuildingTypes.UpdateOneAsync(filter, update);
                    Console.WriteLine($"Building type '{buildingType.Name}' updated successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating building type '{buildingType.Name}': {ex.Message}");
                }
            }
        }
    }

}

