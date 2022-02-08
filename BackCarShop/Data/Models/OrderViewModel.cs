﻿using System.Collections.Generic;

namespace BackCarShop.Data.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public IEnumerable<int> VehicleId_s { get; set; }
    }
}
