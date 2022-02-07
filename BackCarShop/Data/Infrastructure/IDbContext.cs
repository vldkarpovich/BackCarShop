using BackCarShop.Models;
using MongoDB.Driver;
using System.Collections.Generic;

namespace BackCarShop.Data.Infrastructure
{
    public interface IDbContext
    {
        IMongoCollection<Warehouse> Warehouses { get; }
        IMongoCollection<Order> Orders { get; }
        void CreateConnection();
    }
}
