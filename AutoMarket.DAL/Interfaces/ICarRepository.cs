using AutoMarket.Domain.Entity;
using System.Threading.Tasks;

namespace AutoMarket.DAL.Interfaces
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        Task<Car> GetByName(string name);
    }
}
