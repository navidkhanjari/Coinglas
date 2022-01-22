using Academy.Application.Security;
using Academy.Application.Senders;
using Academy.Application.Services.Interfaces;
using Academy.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Academy.Web.Controllers
{
    public class AccountController : SiteBaseController
    {

        #region constructor
        private readonly IUserService _userService;
       
      
        public AccountController(IUserService userService)
        {
            _userService = userService;
           
        }
        #endregion

        #region Register 
        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
        
            if (!ModelState.IsValid)
                return View(register);
            
            var res = await _userService.RegisterUser(register);
            switch (res)
            {
                case RegisterUserResult.UserExist:
                    ModelState.AddModelError("Email", "اطلاعات وارد شده قبلا در سیستم ثبت شده است");
                    break;
                case RegisterUserResult.reCaptchaFalse:
                    ModelState.AddModelError("ConfirmedPassword", "اعتبار سنجی Recaptcha انجام نشد");
                    break;
                case RegisterUserResult.Success:
                    ViewBag.isSuccess = true;
                    return View("Login");
            }
            return View(register);
        }
        #endregion 

        #region Login
        [HttpGet("login")]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated) return Redirect("/");
            return View();
        }
        [HttpPost("login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var res = await _userService.LoginUser(login);

                switch (res)
                {
                    case LoginUserResult.reCaptchaFalse:
                        ModelState.AddModelError("Password", "اعتبار سنجی Recaptcha انجام نشد");
                        break;
                    case LoginUserResult.UserNotExist:
                        ModelState.AddModelError("Email", "کاربری  یافت نشد");
                        break;
                    case LoginUserResult.NotActivated:
                        ModelState.AddModelError("Email", "حساب کاربری شما فعال نشده است");
                        break;
                    case LoginUserResult.WrongPassword:
                        ModelState.AddModelError("Email", "ایمیل یا کلمه عبور اشتباه است");
                        break;
                    case LoginUserResult.Success:
                        var user = await _userService.GetUserByEmail(login.Email);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email,user.Email),
                            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                            new Claim(ClaimTypes.Name,user.UserName)
                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = login.RememberMe
                        };

                        await HttpContext.SignInAsync(principal, properties);


                        return Redirect("/");
                }
            }

            return View(login);
        }
        #endregion

        #region Active Account

        [HttpGet("Account/Active-Account/{id}")]
        public async Task<IActionResult> ActiveAccount(string id)
        {
            ViewBag.isActive = await _userService.ActiveAccount(id);
            return View();
        }
        #endregion

        #region ForgotPassword
        [HttpGet("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgot)
        {
            if (!ModelState.IsValid)
            {
                return View(forgot);
            }
            var res = await _userService.ForgotPassword(forgot);
            switch (res)
            {
                case ForgotPasswordResult.reCaptchaFalse:
                    ModelState.AddModelError("Email", "اعتبار سنجی Recaptcha انجام نشد");
                    break;
                case ForgotPasswordResult.NotFound:
                    ModelState.AddModelError("Email", "کاربری یافت نشد");
                    break;
                case ForgotPasswordResult.Success:
                    ViewBag.IsSuccess = true;
                    return View();
            }
            return View(forgot);
            
           
        
        }
        #endregion

        #region ResetPassword
        [HttpGet("Account/ResetPassword/{id}")]
        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id
            });
        }

        [HttpPost("Account/ResetPassword/{id}")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPassword)
        {
            if (!ModelState.IsValid)
                return View(resetPassword);

            var res =  await _userService.ResetPassword(resetPassword);
            switch (res)
            {

                case ResetPasswordResult.Error:
                    return NotFound();
                case ResetPasswordResult.success:
                    ViewBag.ResPass = true;
                    return View();
            }
            return View(resetPassword);
        }
        #endregion

        #region LogOut
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        #endregion
    }
}
