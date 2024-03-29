﻿using BackCarShop.Data.Infrastructure;
using BackCarShop.Data.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<PagedList<Vehicle>> GetVehiclesAsync(VehicleParameters vehicleParam)
        {
            try
            {
                _dbContext.CreateConnection();
                var builder = new FilterDefinitionBuilder<Warehouse>();
                var filter = builder.Empty; 
                // filter on all warehouses
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

                var result = PagedList<Vehicle>.ToPagedList(vehicles.OrderBy(data => data.Date_added), vehicleParam.PageNumber, vehicleParam.PageSize);
                    //vehicles.Skip((vehicleParam.PageNumber - 1) * vehicleParam.PageSize).Take(vehicleParam.PageSize).OrderBy(data => data.Date_added).ToList();
                return result;
            }
            catch(Exception ex)
            { 
                string log = ex.Message;
                return null;
            }
        }



        public async Task<List<Vehicle>> GetVehiclesAsync()
        {
            try
            {
                _dbContext.CreateConnection();
                var builder = new FilterDefinitionBuilder<Warehouse>();
                var filter = builder.Empty;
                // filter on all warehouses
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
                vehicles.OrderBy(data => data.Date_added);
                return vehicles;
            }
            catch (Exception ex)
            {
                string log = ex.Message;
                return null;
            }
        }


        //added new order
        public async Task CreateOrder(OrderParameters orderParam)
        {
            try
            {
                _dbContext.CreateConnection();
                var builder = new FilterDefinitionBuilder<Basket>();
                var filter = builder.Empty;

                double price = 0;
                var vehicles = await GetVehiclesAsync();
                var result = new List<Vehicle>();
                foreach (var vehivle in vehicles)
                {
                    foreach (var i in orderParam.VehicleId_s)
                    {
                        if (i == vehivle.Id)
                        {
                            price = price + vehivle.Price;
                            result.Add(vehivle);
                        }
                    }
                }

                var order = new Order() { Address = orderParam.Address, Email = orderParam.Email, FirstName = orderParam.FirstName, LastName = orderParam.LastName, Price = price, OrderListVehicle = result };

                await _dbContext.Orders.InsertOneAsync(order);
                await _dbContext.Basket.DeleteOneAsync(filter);

            } 
            catch(Exception ex)
            {
                string log = ex.Message;
            }
        }

        public async Task<int> AddTobasket(int id)
        {
            try
            {
                _dbContext.CreateConnection();
                int count = 0;
                var basket = new Basket();
                var vehicles = await GetVehiclesAsync();

                var builder = new FilterDefinitionBuilder<Basket>();
                var filter = builder.Empty;
                var DbDataBasket = await _dbContext.Basket.Find(filter).ToListAsync();

                if (DbDataBasket != null)
                {
                    foreach (var bs in DbDataBasket)
                    {
                        foreach (var b in bs.Vehicles)
                        {
                            basket.Vehicles.Add(b);
                        }
                        basket.TotalPrice += bs.TotalPrice;
                    }
                }


                if (vehicles != null)
                {
                    foreach (var vehicle in vehicles)
                    {
                        if (vehicle.Id == id)
                        {
                            basket.Vehicles.Add(vehicle);
                            basket.TotalPrice += vehicle.Price;
                            Math.Round(basket.TotalPrice, 2);
                            count = basket.Vehicles.Count();
                            await _dbContext.Basket.DeleteOneAsync(filter);
                            await _dbContext.Basket.InsertOneAsync(basket);
                        }

                    }

                }
                return count;
            }
            catch (Exception ex)
            { 
                string log = ex.Message;
                return 0;
            }
        }

        public async Task<Basket> GetBasket()
        {
            try
            {
                _dbContext.CreateConnection();

                var basket = new Basket();
                var vehicles = await GetVehiclesAsync();

                var builder = new FilterDefinitionBuilder<Basket>();
                var filter = builder.Empty;
                var DbDataBasket = await _dbContext.Basket.Find(filter).ToListAsync();



                if (DbDataBasket != null)
                {
                    foreach (var bs in DbDataBasket)
                    {
                        foreach (var b in bs.Vehicles)
                        {
                            basket.Vehicles.Add(b);
                        }
                        basket.TotalPrice = bs.TotalPrice;
                    }
                }
                return basket;
            }
            catch (Exception ex)
            {
                string log = ex.Message;
                return null;
            }
        }

        public async Task<Basket> DeleteFromBasket(int id)
        {
            try
            {
                _dbContext.CreateConnection();
                var basket = new Basket();

                var builder = new FilterDefinitionBuilder<Basket>();
                var filter = builder.Empty;
                var DbDataBasket = await _dbContext.Basket.Find(filter).ToListAsync();

                if (DbDataBasket != null)
                {
                    foreach (var bs in DbDataBasket)
                    {
                        foreach (var b in bs.Vehicles)
                        {
                            basket.Vehicles.Add(b);
                        }
                        basket.TotalPrice = bs.TotalPrice;
                    }
                }

                if (basket != null)
                {
                    var itemToRemove = basket.Vehicles.First(r => r.Id == id);
                    if (itemToRemove != null)
                    {
                        basket.TotalPrice -= itemToRemove.Price;
                        basket.Vehicles.Remove(itemToRemove);
                    }
                }

                await _dbContext.Basket.DeleteOneAsync(filter);
                await _dbContext.Basket.InsertOneAsync(basket);

                return basket;
            }
            catch (Exception ex)
            {
                string log = ex.Message;
                return null;
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
