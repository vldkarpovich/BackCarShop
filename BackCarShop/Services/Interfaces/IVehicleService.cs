using BackCarShop.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackCarShop.Models
{
    public interface IVehicleService
    {
        Task<PagedList<Vehicle>> GetVehiclesAsync(VehicleParameters vehicleParam);
        Task<List<Vehicle>> GetVehiclesAsync();
        Task<AllVehicleInfoViewModel> GetAllVehicleInfo(int id);
        Task CreateOrder(OrderParameters orderViewModel);
        Task<int> AddTobasket(int id);
        Task<Basket> GetBasket();
        Task<Basket> DeleteFromBasket(int id);
    }
}