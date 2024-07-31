using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameDashboardProject.Domain.Base
{
    public interface IMongoEntity
    {
        [BsonId]
        [BsonElement("_id")]
        ObjectId Id { get; set; }

        [BsonElement("created_at")]
        DateTime CreatedAt { get; set; }

        [BsonElement("updated_at")]
        DateTime? UpdatedAt { get; set; }

        [BsonElement("deleted_at")]
        DateTime? DeletedAt { get; set; }

        [BsonElement("status")]
        DataStatus Status { get; set; }
    }
}
