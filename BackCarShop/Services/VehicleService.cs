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

        public VehicleService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // get all warehouses and all vehicles in this warehouses, or get concret warehouse and vehicle with filter 
        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            try
            {
                _dbContext.CreateConnection();
                var builder = new FilterDefinitionBuilder<Warehouse>();
                var filter = builder.Empty; // filter on all warehouses
                                            //if (!String.IsNullOrWhiteSpace(name))
                                            //{
                                            //    filter = filter & builder.Regex("Name", new BsonRegularExpression(name));
                                            //}
                var warehouses = await _dbContext.Warehouses.Find(filter).ToListAsync();
                var vehicles = new List<Vehicle>();

                foreach (Warehouse warehouse in warehouses)
                {
                    foreach (Vehicle vehicle in warehouse.Cars.Vehicles)
                    {
                        vehicles.Add(vehicle);
                    }
                }
                return vehicles;
            }
            catch(Exception ex)
            { 
                string log = ex.Message;
                return null;
            }
        }
        //added new order
        public async Task CreateOrder(OrderViewModel orderView)
        {
            try
            {
                double price = 0;
                var vehicles = await GetVehiclesAsync();
                var result = new List<Vehicle>();
                foreach (var vehivle in vehicles)
                {
                    foreach (var i in orderView.ids)
                    {
                        if (i == vehivle.Id)
                        {
                            price = price + vehivle.Price;
                            result.Add(vehivle);
                        }
                    }
                }

                var order = new Order() { Address = orderView.Address, Email = orderView.Email, FirstName = orderView.FirstName, LastName = orderView.LastName, Price = price, OrderListVehicle = result };

                await _dbContext.Orders.InsertOneAsync(order);
            } 
            catch(Exception ex)
            {
                string log = ex.Message;
            }
        }

        //get all info about one car
        public async Task<AllVehicleInfoViewModel> GetAllVehicleInfo(int id)
        {
            try
            {
                AllVehicleInfoViewModel allVehicleInfo = new AllVehicleInfoViewModel();
                _dbContext.CreateConnection();
                var builder = new FilterDefinitionBuilder<Warehouse>();
                var filter = builder.Empty; // filter on all warehouses

                var warehouses = await _dbContext.Warehouses.Find(filter).ToListAsync();

                foreach (Warehouse warehouse in warehouses)
                {
                    foreach (Vehicle vehicle in warehouse.Cars.Vehicles)
                    {
                        if (vehicle.Id == id)
                        {
                            allVehicleInfo.WarehouseName = warehouse.Name;
                            allVehicleInfo.CarLocation = warehouse.Cars.Location;
                            allVehicleInfo.Date_added = vehicle.Date_added;
                            allVehicleInfo.Make = vehicle.Make;
                            allVehicleInfo.Model = vehicle.Model;
                            allVehicleInfo.Price = vehicle.Price;
                            allVehicleInfo.Year_model = vehicle.Year_model;
                        }
                    }
                }
                return allVehicleInfo;
            }
            catch(Exception ex)
            {
                string log = ex.Message;
                return null; 
            }
        }
    }
}
