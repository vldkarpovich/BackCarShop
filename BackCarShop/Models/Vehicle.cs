using System;

namespace BackCarShop.Models
{
    public class Vehicle
    {
        public int _id;
        public string make;
        public string model;
        public int year_model;
        public double price;
        public bool licensed;
        public DateTime date_added;


        public Vehicle()
        { }

        public Vehicle(int _id, string make, string model, int year_model, double price, bool licensed, DateTime date_added)
        {
            this._id = _id;
            this.make = make;
            this.model = model;
            this.year_model = year_model;
            this.price = price;
            this.licensed = licensed;
            this.date_added = date_added;

        }
    }
}
