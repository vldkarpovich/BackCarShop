using BackCarShop.Data.Models;
using BackCarShop.Models;
using MongoDB.Driver;

namespace BackCarShop.Data.Infrastructure
{
    public interface IDbContext
    {
        IMongoCollection<Warehouse> Warehouses { get; }
        IMongoCollection<Order> Orders { get; }
        IMongoCollection<Basket> Basket { get; }
        void CreateConnection();
    }
}
