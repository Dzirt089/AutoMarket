using AutoMarket.Domain.Entity;
using AutoMarket.Domain.Response;

namespace AutoMarket.Service.Interfaces
{
    public interface ICarService
    {
        Task<IBaseResponse<IEnumerable<Car>>> GetCars();
    }
}
