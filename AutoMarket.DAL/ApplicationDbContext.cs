using AutoMarket.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AutoMarket.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Car> Car { get; set; }
    }
}
