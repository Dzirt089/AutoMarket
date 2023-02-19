using AutoMarket.DAL.Interfaces;
using AutoMarket.Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AutoMarket.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var response = await _carRepository.GetAll();
            var response1 = await _carRepository.GetByName("Vaz 2107");
            var response2 = await _carRepository.Get(3);

            var car = new Car()
            {
                Id = 4,
                Name = "Vaz21114",
                Model = "Vaz",
                Speed = 140,
                Price = 150000,
                Description = "Ваз",
                DateCreate = System.DateTime.Now
            };
            await _carRepository.Create(car);
            var response3 = _carRepository.Delete(car);
            return View(response);
        }
    }
}
