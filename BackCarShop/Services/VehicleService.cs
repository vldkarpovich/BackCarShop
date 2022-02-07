using BackCarShop.Data.Infrastructure;
using BackCarShop.Data.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackCarShop.Models
{
    public class VehicleService : IVehicleService
    {
        private IDbContext _dbContext; // Data bases
        private List<Vehicle> vehicles = new List<Vehicle>();
        private List<Warehouse> warehouses = new List<Warehouse>();

        public VehicleService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // get all warehouses and all vehicles in this warehouses, or get concret warehouse and vehicle with filter 
        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            _dbContext.CreateConnection();
            var builder = new FilterDefinitionBuilder<Warehouse>();
            var filter = builder.Empty; // filter on all warehouses
            //if (!String.IsNullOrWhiteSpace(name))
            //{
            //    filter = filter & builder.Regex("Name", new BsonRegularExpression(name));
            //}
            warehouses = await _dbContext.Warehouses.Find(filter).ToListAsync();

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
        //added new order
        public async Task CreateOrder(string fn, string ln, string email, string adres, int[] ids)
        {
            try
            {
                double price = 0;
                if (vehicles != null)
                {
                    List<Vehicle> result = new List<Vehicle>();
                    foreach (var vehivle in vehicles)
                    {
                        foreach (var i in ids)
                        {
                            if (i == vehivle._id)
                            {
                                price = price + vehivle.price;
                                result.Add(vehivle);
                            }
                        }
                    }

                    Order order = new Order() { adres = adres, email = email, firstName = fn, lastName = ln, price = price, orderListVehicle = result };

                    await _dbContext.Orders.InsertOneAsync(order);
                }
            }
            catch(Exception ex)
            {
                string log = ex.Message;
            }
        }

        //get all info about one car
        public async Task<AllVehicleInfo> GetAllVehicleInfo(int id)
        {
            AllVehicleInfo allVehicleInfo = new AllVehicleInfo();

            foreach (Warehouse warehouse in warehouses)
            {
                foreach (Vehicle vehicle in warehouse.cars.vehicles)
                {
                    if (vehicle._id == id)
                    {
                        allVehicleInfo.warehouseName = warehouse.name;
                        allVehicleInfo.carLocation = warehouse.cars.location;
                        allVehicleInfo.date_added = vehicle.date_added;
                        allVehicleInfo.make = vehicle.make;
                        allVehicleInfo.model = vehicle.model;
                        allVehicleInfo.price = vehicle.price;
                        allVehicleInfo.year_model = vehicle.year_model;
                    }
                }
            }
            return allVehicleInfo;
        }
    }
}
