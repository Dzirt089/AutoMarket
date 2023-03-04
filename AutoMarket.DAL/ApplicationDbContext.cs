using AutoMarket.Domain.Entity;
using AutoMarket.Domain.Enum;
using AutoMarket.Services;
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
                        Password = Hach_md5.hashPassword("123456"),
                        Role = Role.Admin
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "Moderator",
                        Password = Hach_md5.hashPassword("654321"),
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
                        Description = @"Четвёртое поколение знаменитого среднеразмерного кроссовера BMW X5 немецкой компании BMW. 
                                      Выпуск модели был начат в ноябре 2018 года в Европе. 
                                      Одновременно с запуском новой модели с производства была снята предыдущая - F15",
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
