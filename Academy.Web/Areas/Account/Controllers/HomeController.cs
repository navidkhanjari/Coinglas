using Academy.Application.Services.Interfaces;
using Academy.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Academy.Web.Areas.Account.Controllers
{
    public class HomeController : AccountBaseController
    {
        #region constructor
        private readonly IUserService _userService;
        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Index
        public async Task<IActionResult> Index()
        {
            return View(await _userService.GetUserInformationByUserName(User.Identity.Name));
        }
        #endregion

        #region Change User Password 

        [HttpGet("Account/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost("Account/ChangePassword"),ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordViewModel changePass)
        {
            if (!ModelState.IsValid)
                return View(changePass);

            var CurrentUserName = User.Identity.Name;

            var res = await _userService.ChangeUserPassword(CurrentUserName, changePass);

            switch (res)
            {
                case ChangeUserPasswordResult.OldPasswordIsWrong:
                    ModelState.AddModelError("OldPassword", "کلمه عبور فعلی اشتباه وارد شده است");
                    break;
                case ChangeUserPasswordResult.Success:
                    ViewBag.IsSuccess = true;
                    return View();

            }
            return View(changePass);
        }
        #endregion
    }
}
