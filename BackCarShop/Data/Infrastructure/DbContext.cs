using BackCarShop.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BackCarShop.Data.Infrastructure
{
    public class DbContext : IDbContext
    {
        private IMongoDatabase _database;

        private readonly IConfiguration _configuration;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Call the Warehouses colection 
        public IMongoCollection<Warehouse> Warehouses
        { get { return _database.GetCollection<Warehouse>("Warehouses"); } }
        // Call the Orders colection
        public IMongoCollection<Order> Orders
        { get { return _database.GetCollection<Order>("Orders"); } }

        public IList<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

        public void CreateConnection()
        {
            // string conection 
            var connectionString = _configuration.GetConnectionString("MongoDb");
            var connection = new MongoUrlBuilder(connectionString);
            // get a client to interact with the database
            MongoClient client = new MongoClient(connectionString);
            // get access to the database itself
            _database = client.GetDatabase(connection.DatabaseName);
        }
    }
}
