using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BackCarShop.Models
{
    public class Vehicle
    {
        [BsonElement("_id")]
        public int Id { get; set; }
        [BsonElement("make")]
        public string Make { get; set; }
        [BsonElement("model")]
        public string Model { get; set; }
        [BsonElement("year_model")]
        public int Year_model { get; set; }
        [BsonElement("price")]
        public double Price { get; set; }
        [BsonElement("licensed")]
        public bool Licensed { get; set; }
        [BsonElement("date_added")]
        public DateTime Date_added { get; set; }
    }
}
