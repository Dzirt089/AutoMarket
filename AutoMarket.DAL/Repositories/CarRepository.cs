using AutoMarket.DAL.Interfaces;
using AutoMarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoMarket.DAL.Repositories
{
    public class CarRepository : ICarRepository
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

        public async Task<List<Car>> GetAll()
        {
            return await _context.Car.ToListAsync();
        }

        public Task<Car> GetByName(string name)
        {
            return _context.Car.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Car> Update(Car entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
