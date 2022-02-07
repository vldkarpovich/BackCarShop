using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackCarShop.Models
{
    public class Order
    {
        public int _id { get; set; }
        public List<Vehicle> orderListVehicle { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string adres { get; set; }
        public string email { get; set; }
        public double price { get; set; }
    }
}
