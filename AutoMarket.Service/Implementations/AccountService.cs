using AutoMarket.DAL.Interfaces;
using AutoMarket.Domain.Entity;
using AutoMarket.Domain.Enum;
using AutoMarket.Domain.Response;
using AutoMarket.Domain.ViewModel.Account;
using AutoMarket.Service.Interfaces;
using AutoMarket.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AutoMarket.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountService _accountService;
        private readonly IBaseRepository<User> _userRepository;

        public AccountService(IAccountService accountService, IBaseRepository<User> userRepository)
        {
            _accountService = accountService;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Пользовательне не найден"
                    };
                }
                if (user.Password != Hach_md5.HashPassword(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Неверный пароль или логин"
                    };
                }
                var result = Authenticate(user);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = Domain.Enum.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity> { Description = ex.Message, StatusCode = Domain.Enum.StatusCode.InternalServerError };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Данный пользователь уже существует"
                    };
                }
                user = new User()
                {
                    Name = model.Name,
                    Role = Role.User,
                    Password = Hach_md5.HashPassword(model.Password)
                };
                await _userRepository.Create(user);

                var result = Authenticate(user);
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Пользователь добавлен!",
                    StatusCode = Domain.Enum.StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity> { Description = ex.Message, StatusCode = Domain.Enum.StatusCode.InternalServerError };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            ClaimsIdentity claimsIndentity = new ClaimsIdentity(claims, "ApplicationCookies", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIndentity;
        }
    }
}
