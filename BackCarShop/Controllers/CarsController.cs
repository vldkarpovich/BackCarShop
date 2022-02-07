using BackCarShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime;
using Microsoft.AspNetCore.Cors;

namespace BackCarShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private VehicleService cars = new VehicleService();
        private string name = null;

        //[EnableCors("AnotherPolicy")]
        [HttpGet]
        public async Task<IActionResult> GetVehicles(string name)
        {
            try 
            {
                var result = await cars.GetVehiclesAsync(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                //var log = ex.Message;
                return new BadRequestResult();
            }
        }

    }
}
