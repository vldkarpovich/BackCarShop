using BackCarShop.Data.Models;
using BackCarShop.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<IActionResult> GetVehicles([FromQuery] VehicleParameters vehicleParameters)
        {
            try
            {
                var result = await _vehicleService.GetVehiclesAsync(vehicleParameters);

                var metadata = new
                {
                    result.TotalCount,
                    result.PageSize,
                    result.CurrentPage,
                    result.TotalPages,
                    result.HasNext,
                    result.HasPrevious
                };

                Response.Headers.Add("x-total-count", JsonConvert.SerializeObject(metadata.TotalCount));
                Response.Headers.Add("x-page-size", JsonConvert.SerializeObject(metadata.PageSize));
                Response.Headers.Add("x-current-page", JsonConvert.SerializeObject(metadata.CurrentPage));
                Response.Headers.Add("x-total-pages", JsonConvert.SerializeObject(metadata.TotalPages));
                Response.Headers.Add("x-has-next", JsonConvert.SerializeObject(metadata.HasNext));
                Response.Headers.Add("x-has-previous", JsonConvert.SerializeObject(metadata.HasPrevious));

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


        [HttpGet]
        [Route("getvehicle")]
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
        [Route("posttobasket")]
        public async Task<IActionResult> PostToBasket(int id)
        {
            try
            {
                var result = await _vehicleService.AddTobasket(id);

                return Ok(result);
            }
            catch (Exception Ex)
            { 
                var log = Ex.Message;
                return new BadRequestResult();
            }
        }

        [HttpGet]
        [Route("getbasket")]
        public async Task<IActionResult> GetBasket()
        {
            try
            {
                var result = await _vehicleService.GetBasket();
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

        [HttpDelete]
        [Route("deletebasket")]
        public async Task<IActionResult> DeleteBasket(int id)
        {
            try
            {
                var result = await _vehicleService.DeleteFromBasket(id);
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


        [HttpPut]
        [Route("poscreateorder")]
        public async Task<IActionResult> PutCreateOrder(OrderParameters orderViewModel)
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
