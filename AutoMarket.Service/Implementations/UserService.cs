using AutoMarket.DAL.Interfaces;
using AutoMarket.Domain.Entity;
using AutoMarket.Domain.Enum;
using AutoMarket.Domain.Response;
using AutoMarket.Domain.ViewModel.User;
using AutoMarket.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMarket.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _repository;

        public UserService(IBaseRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<IBaseResponse<User>> Create(UserViewModel model)
        {
            try
            {
                var user = await _repository.GetAll().FirstOrDefaultAsync(x=>x.Name == model.Name);
                if (user != null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "Такой пользователь уже существует",
                        StatusCode = StatusCode.UserAlreadyExist
                    };
                }

                user = new User()
                {
                    Name = model.Name,
                    Password = Hach_md5.hashPassword(model.Password),
                    //Role = (Role)Convert.ToInt32(model.Role),
                    Role = Enum.Parse<Role>(model.Role),
                };
                await _repository.Create(user);
                return new BaseResponse<User>()
                {
                    Data = user,
                    Description = "Пользователь добавлен",
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }

        }

        public async Task<IBaseResponse<bool>> DeleteUser(int id)
        {
            try
            {
                var user = await _repository.GetAll().FirstOrDefaultAsync(x=> x.Id == id);
                if (user == null) 
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Пользователь не найден",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                
                await _repository.Delete(user);
                return new BaseResponse<bool>()
                {
                    Description = "Пользователь удален",
                    StatusCode = StatusCode.OK,
                    Data = true
                };
                
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
                var roles = ((Role[])Enum.GetValues(typeof(Role)))
                .ToDictionary(k => (int)k, v => v.GetType().ToString());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode= StatusCode.OK,
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description= ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = await _repository.GetAll().Select(x=> new UserViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Role = x.Role.GetType().ToString(),
                }).ToListAsync();
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
