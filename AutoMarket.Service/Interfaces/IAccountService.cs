using AutoMarket.Domain.Response;
using AutoMarket.Domain.ViewModel.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
        Task<BaseResponse<ClaimsIdentity>> Login (LoginViewModel model);
    }
}
