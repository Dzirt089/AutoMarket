﻿using AutoMarket.DAL.Interfaces;
using AutoMarket.Domain.Entity;
using AutoMarket.Domain.Enum;
using AutoMarket.Domain.Response;
using AutoMarket.Domain.ViewModel.Car;
using AutoMarket.Service.Interfaces;

namespace AutoMarket.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<IBaseResponse<Car>> CreateCar(CarViewModel carViewModel)
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
                return new BaseResponse<Car>()
                {
                    StatusCode = StatusCode.OK,
                    Data = car
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
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
                var car = await _carRepository.Get(id);
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
                return new BaseResponse<bool>() { Description = $"[DeleteCar] : {ex.Message}" ,StatusCode = StatusCode.InternalServerError};
            }
        }


        public async Task<IBaseResponse<Car>> GetCarByName(string name)
        {
            var baseResponse = new BaseResponse<Car>();
            try
            {
                var car = await _carRepository.GetByName(name);
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
                var car = await _carRepository.Get(id);
                if (car == null) 
                {
                    baseResponse.Description = "User not found";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.Data = car;
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<Car>() { Description = $"[GetCar] : {ex.Message}" };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Car>>> GetCars()
        {
            var baseResponse = new BaseResponse<IEnumerable<Car>>();
            try
            {
                var cars = await _carRepository.GetAll();
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
    }
}
