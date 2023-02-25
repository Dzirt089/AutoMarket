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

        public async Task<bool> Create(Car entity)
        {
            await _context.Car.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Car entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<Car> Get(int id)
        {
            return await _context.Car.FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Car> GetByName(string name)
        {
            return await _context.Car.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Car> Update(Car entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        IQueryable<Car> IBaseRepository<Car>.GetAll()
        {
            return (IQueryable<Car>)_context.Car.ToListAsync();
        }
    }
}
