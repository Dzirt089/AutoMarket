using AutoMarket.DAL.Interfaces;
using AutoMarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMarket.DAL.Repositories
{
    public class CarRepository : IBaseRepository<Car>
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Create(Car entity)
        {
            await _context.Car.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Car entity)
        {
            _context.Car.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<Car> Update(Car entity)
        {
            _context.Car.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public IQueryable<Car> GetAll()
        {
            return _context.Car;
        }
    }
}
