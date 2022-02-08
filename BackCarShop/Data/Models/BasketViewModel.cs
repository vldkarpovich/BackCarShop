using BackCarShop.Models;
using System.Collections.Generic;

namespace BackCarShop.Data.Models
{
    public class BasketViewModel
    {
        public List<Vehicle> Vehicles { get; set; }
        public double TotalPrice { get; set; }
    }
}
