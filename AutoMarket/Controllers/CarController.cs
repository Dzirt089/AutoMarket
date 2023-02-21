using AutoMarket.DAL.Interfaces;
using AutoMarket.Domain.Entity;
using AutoMarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoMarket.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {    
            var response = await _carService.GetCars();
           
            return View(response.Data);
        }
    }
}
