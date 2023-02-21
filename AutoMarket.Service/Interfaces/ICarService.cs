using AutoMarket.Domain.Entity;
using AutoMarket.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Service.Interfaces
{
    public interface ICarService
    {
        Task<IBaseResponse<IEnumerable<Car>>> GetCars();
    }
}
