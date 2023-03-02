using AutoMarket.Domain.ViewModel.User;
using AutoMarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMarket.Controllers
{
    public class UseController : Controller
    {
        private readonly IUserService _userService;

        public UseController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public JsonResult GetRoles()
        {
            var roles = _userService.GetRoles();
            return Json(roles.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.Create(model);
                if(response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { descriptor = response.Description });
                }
                return BadRequest(new {errorMessage = response.Description});
            }
            var errorMessage = ModelState.Values.SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList();
            return RedirectToAction("Index", "Home", new {errorMessage});
        }


        public async Task<IActionResult> GetUsers()
        {
            var response = await _userService.GetUsers();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetUsers");
            }

            return RedirectToAction("Index", "Home");

        }
    }
}
