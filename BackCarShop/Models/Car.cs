using System.Collections.Generic;

namespace BackCarShop.Models
{
    public class Car
    {
        public string location;
        public List<Vehicle> vehicles;

        public Car()
        { }

        public Car(string location, List<Vehicle> vehicles)
        {
            this.location = location;
            this.vehicles = vehicles;
        }
    }
}
