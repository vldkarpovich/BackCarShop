using System;

namespace BackCarShop.Data.Models
{
    public class AllVehicleInfoViewModel
    {
        public string WarehouseName { get; set; }
        public string CarLocation { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year_model { get; set; }
        public double Price { get; set; }
        public DateTime Date_added { get; set; }
    }
}
