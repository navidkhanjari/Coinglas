using Academy.Application.Security;
using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        #region constructor
        private readonly IUserService _userService;
        private readonly ISubscribeService _subscribeService;

        public HomeController(IUserService userService,ISubscribeService subscribeService)
        {
            _userService = userService;
            _subscribeService = subscribeService;
        }
        #endregion

        #region Index

        [PermissionChecker(1)]
        [HttpGet("Admin/Home/Index")]
        [HttpGet("Admin")]
        public async Task<IActionResult> IndexAsync()
        {
            ViewData["UserCount"] = await _userService.CountUsers();
            ViewData["SubscribeCount"] = await _subscribeService.SubscribeCount();
            var newUsers = await _userService.GetNewUsersForAdmin();
            return View(newUsers);
        }
        #endregion
    }
}
