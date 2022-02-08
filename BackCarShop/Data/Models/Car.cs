using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BackCarShop.Models
{
    public class Car
    {
        [BsonElement("location")]
        public string Location { get; set; }
        [BsonElement("vehicles")]
        public List<Vehicle> Vehicles { get; set; }
}
}
