using AutoMarket.DAL.Interfaces;
using AutoMarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public bool Create(Car entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Car entity)
        {
            throw new NotImplementedException();
        }

        public Car Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Car>> GetAll()
        {
            return _context.Car.ToListAsync();
        }

        public Car GetByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
