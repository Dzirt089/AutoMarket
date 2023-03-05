using AutoMarket.DAL.Interfaces;
using AutoMarket.DAL.Repositories;
using AutoMarket.Domain.Entity;
using AutoMarket.Service.Implementations;
using AutoMarket.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoMarket
{
    public static class StartupInit
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {           
            services.AddScoped<IBaseRepository<Car>, CarRepository>();
            services.AddScoped<IBaseRepository<User>, UserRepository>();
        }
        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}
