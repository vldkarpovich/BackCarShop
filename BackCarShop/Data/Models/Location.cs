
using MongoDB.Bson.Serialization.Attributes;

namespace BackCarShop.Models
{
    public class Location
    {
        public double lat { get; set; }
        [BsonElement("long")]
        public double _long { get; set; }
    }
}
