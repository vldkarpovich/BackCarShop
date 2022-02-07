using BackCarShop.Data.Models;
using BackCarShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetVehicles()
        {
            try
            {
                var result = await _vehicleService.GetVehiclesAsync();
                if (result != null)
                {
                    return Ok(result);
                }
                else { return new NotFoundResult(); }
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
                var result = await _vehicleService.GetAllVehicleInfo(id);
                if (result != null)
                {
                    return Ok(result);
                }
                else { return new NotFoundResult(); }
            }
            catch (Exception ex)
            {
                //var log = ex.Message;
                return new BadRequestResult();
            }
        }



        [HttpPost]
        public async Task<IActionResult> PostCreateOrder(OrderViewModel orderViewModel)
        {
            try
            {
                if (orderViewModel != null)
                {
                    await _vehicleService.CreateOrder(orderViewModel);
                    return new OkResult();
                }
                else { return new NotFoundResult(); }
            }
            catch(Exception ex)
            { 
                return new BadRequestResult(); 
            }
        }

    }
}
