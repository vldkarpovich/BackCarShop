using BackCarShop.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackCarShop.Models
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetVehiclesAsync();
        Task<AllVehicleInfoViewModel> GetAllVehicleInfo(int id);
        Task CreateOrder(OrderViewModel orderViewModel);
    }
}