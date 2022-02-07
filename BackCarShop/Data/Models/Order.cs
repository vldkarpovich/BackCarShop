using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackCarShop.Models
{
    public class Order
    {
        public List<Vehicle> orderListVehicle { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string adres { get; set; }
        public string email { get; set; }
        public int price { get; set; }



    }
}
