using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace BackCarShop.Models
{
    public class VehicleService
    {
        IMongoDatabase database; // Data bases
        List<Vehicle> vehicles = new List<Vehicle>();
        List<Warehouse> warehouses = new List<Warehouse>();

        public VehicleService() 
        {
            // string conection 
            //string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            string connectionString = "mongodb://localhost:27017/OlineShop";
            var connection = new MongoUrlBuilder(connectionString);
            // получаем клиента для взаимодействия с базой данных
            MongoClient client = new MongoClient(connectionString);
            // получаем доступ к самой базе данных
            database = client.GetDatabase(connection.DatabaseName);
        }

        // Call the Warehouses colection 
        public IMongoCollection<Warehouse> Warehouses
        {
            get { return database.GetCollection<Warehouse>("Warehouses"); }
        }

        // get all warehouses and all vehicles in this warehouses, or get concret warehouse and vehicle with filter 
        public async Task<List<Vehicle>> GetVehiclesAsync(string name)
        {

            var builder = new FilterDefinitionBuilder<Warehouse>();
            var filter = builder.Empty; // filter on all warehouses
            if (!String.IsNullOrWhiteSpace(name))
            {
                filter = filter & builder.Regex("Name", new BsonRegularExpression(name));
            }
            warehouses = await Warehouses.Find(filter).ToListAsync();

            foreach (Warehouse warehouse in warehouses)
            {
                foreach (Vehicle vehicle in warehouse.cars.vehicles)
                {
                    vehicles.Add(vehicle);
                }
            }

            //return await Warehouses.Find().ToListAsync();
            return vehicles;
        }
    }
}
