using BackCarShop.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BackCarShop.Data.Models
{
    public class Basket
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("vehicles")]
        public List<Vehicle> Vehicles = new List<Vehicle>();
        [BsonElement("totalPrice")]
        public double TotalPrice { get; set; }
    }
}
