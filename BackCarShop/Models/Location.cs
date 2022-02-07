
namespace BackCarShop.Models
{
    public class Location
    {
        public double lat;
        public double _long;

        public Location()
        { }
        public Location(double lat, double _long)
        {
            this.lat = lat;
            this._long = _long;
        }
    }
}
