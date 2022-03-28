using BackCarShop.Data.Models;
using BackCarShop.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

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

        // Call the Basket colection
        public IMongoCollection<Basket> Basket
        { get { return _database.GetCollection<Basket>("Basket"); } }

        public IList<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

        public void CreateConnection()
        {
            //string conection 
            //var connectionString = _configuration.GetConnectionString("MongoDb");


            string connectionString = _configuration.GetConnectionString("MongoDb");
            var connection = new MongoUrlBuilder(connectionString);

            //MongoClientSettings settings = MongoClientSettings.FromUrl(
            //  new MongoUrl(connectionString)
            //);
            //settings.SslSettings =
            //  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            //var mongoClient = new MongoClient(settings);



            ////var cert = new X509Certificate2("client.pfx", "mySuperSecretPassword");

            ////var settings = new MongoClientSettings
            ////{
            ////    SslSettings = new SslSettings
            ////    {
            ////        ClientCertificates = new[] { cert },
            ////    },
            ////    UseSsl = true
            ////};
            // get a client to interact with the database
            MongoClient client = new MongoClient(connectionString);
            // get access to the database itself
            _database = client.GetDatabase(connection.DatabaseName);
        }
    }
}
