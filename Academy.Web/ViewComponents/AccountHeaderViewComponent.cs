using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.ViewComponents
{
    public class AccountHeaderViewComponent:ViewComponent
    {
        #region constructor
        private readonly IUserService _userService;
        public AccountHeaderViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        #endregion
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("AccountHeader",await _userService.GetUserInformationByUserName(User.Identity.Name));
        }
    
    }

}
