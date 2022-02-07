
namespace BackCarShop.Models
{
    public class Warehouse
    {
        public string _id;
        public string name;
        public Location location;
        public Car cars;

        public Warehouse()
        { }

        public Warehouse(string _id, string name, Location location, Car cars)
        {
            this._id = _id;
            this.name = name;
            this.location = location;
            this.cars = cars;
        }
    }
}
