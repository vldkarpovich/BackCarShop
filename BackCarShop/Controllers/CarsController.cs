using BackCarShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime;

namespace BackCarShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public CarsController(IVehicleService vehicle)
        {
            _vehicleService = vehicle;
        }


        //// GET: api/<VehicleController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            try
            {
                var result = await _vehicleService.GetVehiclesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                //var log = ex.Message;
                return new BadRequestResult();
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            try
            {
                Vehicle vehicle1 = null;
                var result = await _vehicleService.GetVehiclesAsync();
                foreach (var vehicle in result)
                {
                    if (vehicle._id == id)
                    { vehicle1 = vehicle; }
                }
                if (vehicle1 != null)
                {
                    return Ok(vehicle1);
                }
                else { return new NotFoundResult(); }
            }
            catch (Exception ex)
            {
                //var log = ex.Message;
                return new BadRequestResult();
            }
        }

        //[HttpPost]
        //public async Task<>

        //[HttpGet]
        //public async Task<IActionResult> GetVehicles(string name)
        //{
        //    try
        //    {
        //        var result = await cars.GetVehiclesAsync(name);
        //        //var json = JsonSerializer.Serialize(result);
        //        var model = new VehicleList { Vehicles = result };
        //        //return View(model);
        //        return Ok(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        //var log = ex.Message;
        //        return new BadRequestResult();
        //    }
        //}

    }
}
