
using MongoDB.Bson.Serialization.Attributes;

namespace BackCarShop.Models
{
    public class Location
    {
        [BsonElement("lat")]
        public double Lat { get; set; }
        [BsonElement("long")]
        public double Long { get; set; }
    }
}
