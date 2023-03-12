using AutoMarket.Domain.Filter;
using AutoMarket.Domain.ViewModel.Car;
using AutoMarket.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMarket.Controllers
{
    [ValidateModel, HandleException, FeatureEnabled(IsEnabled = true)]
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
            return View(response.Data.ToList());
            
        }

        [HttpGet]
        public async Task<IActionResult> GetCar(int id)
        {
            var response = await _carService.GetCar(id);
            return View(response.Data);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _carService.DeleteCar(id);
            return RedirectToAction("GetCars");
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return View();
            
            var response = await _carService.GetCar(id);
            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Save(CarViewModel model)
        {
            ModelState.Remove("DataCreate");

            if (model.Id == 0)
            {
                byte[] imageData;
                using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                }
                await _carService.CreateCar(model, imageData);
            }
            else
            {
                await _carService.Edit(model.Id, model);
            }

            return RedirectToAction("GetCars");
        }


        [HttpPost]
        public JsonResult GetTypes()
        {
            var types = _carService.GetTypes();
            return Json(types);
        }
    }
}
