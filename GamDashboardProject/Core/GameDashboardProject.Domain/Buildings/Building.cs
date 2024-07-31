using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using GameDashboardProject.Domain.Base;

namespace GameDashboardProject.Domain.Buildings
{
    public class Building : MongoBaseEntity
    {
        [BsonElement("user_id")]
        public Guid UserId { get; set; }

        [BsonElement("building_type_id")]
        public string BuildingTypeId { get; set; }

        [BsonElement("building_cost")]
        public decimal BuildingCost { get; set; }

        [BsonElement("construction_time")]
        public int ConstructionTime { get; set; }
    }
}
