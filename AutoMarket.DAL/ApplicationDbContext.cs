using AutoMarket.Domain.Entity;
using AutoMarket.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using System;

namespace AutoMarket.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> Car { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);
                builder.HasData(new User[]
                {
                    new User()
                    {
                        Id = 1,
                        Name = "Admin",
                        Password = "123456",
                        Role = Role.Admin
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "Moderator",
                        Password = "654321",
                        Role = Role.Moderator
                    }
                });
                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
                builder.Property(x => x.Password).IsRequired();

            });

            modelBuilder.Entity<Car>(builder =>
            {
                builder.ToTable("Car").HasKey(x => x.Id);
                builder.HasData(new Car[]
                {
                    new Car()
                    {
                        Id = 1,
                        Name = "Germany AUTO",
                        Description = "BMW X5",
                        Model = "BMW X5",
                        Speed = 280,
                        Price = 5999000,
                        DateCreate = DateTime.Now,
                        Avatar = null,
                        TypeCar = TypeCar.PassengerCar
                    }
                });
            });
        }


    }
}
