using AutoMarket.DAL.Interfaces;
using AutoMarket.Domain.Entity;
using AutoMarket.Domain.Enum;
using AutoMarket.Domain.Response;
using AutoMarket.Domain.ViewModel.Car;
using AutoMarket.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace AutoMarket.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly IBaseRepository<Car> _carRepository;

        public CarService(IBaseRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IBaseResponse<CarViewModel>> CreateCar(CarViewModel carViewModel, byte[] imageData)
        {
            try
            {
                var car = new Car()
                {
                    Description = carViewModel.Description,
                    DateCreate = carViewModel.DateCreate,
                    Speed = carViewModel.Speed,
                    Model = carViewModel.Model,
                    Price = carViewModel.Price,
                    Name = carViewModel.Name,
                    TypeCar = (TypeCar)Convert.ToInt32(carViewModel.TypeCar)
                };
                await _carRepository.Create(car);
                return new BaseResponse<CarViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    //Data = car
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CarViewModel>()
                {
                    Description = $"[CreateCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }


        public async Task<IBaseResponse<bool>> DeleteCar(int id)
        {
            var baseResponse = new BaseResponse<bool>() { Data = true };
            try
            {
                var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Id==id);
                if (car == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                await _carRepository.Delete(car);
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>() { Description = $"[DeleteCar] : {ex.Message}", StatusCode = StatusCode.InternalServerError };
            }
        }


        public async Task<IBaseResponse<Car>> GetCarByName(string name)
        {
            var baseResponse = new BaseResponse<Car>();
            try
            {
                var car = await _carRepository.GetAll().FirstOrDefaultAsync(x=>x.Name == name);
                if (car == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = car;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>() { Description = $"[GetCarByName] : {ex.Message}" };
            }
        }


        public async Task<IBaseResponse<Car>> GetCar(int id)
        {
            var baseResponse = new BaseResponse<Car>();
            try
            {
                var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (car == null)
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = car;
                baseResponse.StatusCode = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>() { Description = $"[GetCar] : {ex.Message}" };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
        {
            var baseResponse = new BaseResponse<IEnumerable<Car>>();
            try
            {
                var cars = await _carRepository.GetAll().ToListAsync();
                if (cars.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = cars;
                baseResponse.StatusCode = StatusCode.OK;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Car>>() { Description = $"[GetCars] : {ex.Message}" };

            }
        }

        public async Task<IBaseResponse<Car>> Edit(int id, CarViewModel model)
        {
            var baseResponse = new BaseResponse<Car>();
            try
            {
                var car = await _carRepository.GetAll().FirstOrDefaultAsync(x=>x.Id==id);
                if (car == null)
                {
                    baseResponse.Description = "Car not found";
                    baseResponse.StatusCode = StatusCode.CarNotFound;
                    return baseResponse;
                }
                car.Price = model.Price;
                car.Description = model.Description;
                car.TypeCar = (TypeCar)Convert.ToInt32(model.TypeCar);
                car.Name = model.Name;
                car.Model = model.Model;
                car.Speed = model.Speed;
                car.DateCreate = model.DateCreate;

                await _carRepository.Update(car);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>() { Description = $"[Edit] : {ex.Message}", StatusCode = StatusCode.InternalServerError };
            }
        }
    }
}
