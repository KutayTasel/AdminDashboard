//using MongoDB.Bson.Serialization.Attributes;
//using MongoDB.Bson;
//using System.Collections.Generic;
using GameDashboardProject.Domain.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameDashboardProject.Domain.Buildings
{
    public class BuildingType : MongoBaseEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("is_open")]
        public bool IsOpen { get; set; }
    }
}
