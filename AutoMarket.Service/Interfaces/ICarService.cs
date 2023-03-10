using AutoMarket.Domain.Entity;
using AutoMarket.Domain.Response;
using AutoMarket.Domain.ViewModel.Car;

namespace AutoMarket.Service.Interfaces
{
    public interface ICarService
    {
        Task<IBaseResponse<IEnumerable<Car>>> GetCars();
        Task<IBaseResponse<Car>> GetCar(int id);
        Task<IBaseResponse<Car>> CreateCar(CarViewModel carViewModel, byte[] imageData);
        Task<IBaseResponse<bool>> DeleteCar(int id);
        Task<IBaseResponse<Car>> GetCarByName(string name);
        Task<IBaseResponse<Car>> Edit(int id, CarViewModel model);
        BaseResponse<Dictionary<int, string>> GetTypes();
    }
}
