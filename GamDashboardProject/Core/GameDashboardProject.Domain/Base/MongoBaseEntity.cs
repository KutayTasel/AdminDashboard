using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameDashboardProject.Domain.Base
{
    public abstract class MongoBaseEntity : IMongoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonIgnore]
        public string IdString
        {
            get => Id.ToString();
            set => Id = ObjectId.Parse(value);
        }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updated_at")]
        public DateTime? UpdatedAt { get; set; }

        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }

        [BsonElement("status")]
        public DataStatus Status { get; set; }
    }
}
