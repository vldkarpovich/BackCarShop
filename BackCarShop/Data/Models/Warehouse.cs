
using MongoDB.Bson.Serialization.Attributes;

namespace BackCarShop.Models
{
    public class Warehouse
    {
        [BsonElement("_id")]
        public string Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("location")]
        public Location Location { get; set; }
        [BsonElement("cars")]
        public Car Cars { get; set; }
    }
}
