using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BackCarShop.Models
{
    public class Order
    {
        [BsonElement("_id")]
        public int Id { get; set; }
        [BsonElement("orderListVehicle")]
        public List<Vehicle> OrderListVehicle { get; set; }
        [BsonElement("firstName")]
        public string FirstName { get; set; }
        [BsonElement("lastName")]
        public string LastName { get; set; }
        [BsonElement("address")]
        public string Address { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("price")]
        public double Price { get; set; }
    }
}
