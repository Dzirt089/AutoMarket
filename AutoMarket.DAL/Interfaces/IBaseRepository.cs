using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoMarket.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);

        Task<T> Get(int id);

        Task<List<T>> GetAll();

        Task<bool> Delete(T entity);
    }
}
