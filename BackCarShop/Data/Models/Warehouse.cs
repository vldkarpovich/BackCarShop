
namespace BackCarShop.Models
{
    public class Warehouse
    {
        public string _id { get; set; }
        public string name { get; set; }
        public Location location { get; set; }
        public Car cars { get; set; }
    }
}
