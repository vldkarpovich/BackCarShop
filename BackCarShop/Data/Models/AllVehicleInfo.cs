using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackCarShop.Data.Models
{
    public class AllVehicleInfo
    {
        public string warehouseName { get; set; }
        public string carLocation { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public int year_model { get; set; }
        public double price { get; set; }
        public DateTime date_added { get; set; }
    }
}
