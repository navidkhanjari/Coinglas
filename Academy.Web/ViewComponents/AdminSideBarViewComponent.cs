
using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Academy.Web.ViewComponents
{
    public class AdminSideBarViewComponent:ViewComponent
    {
        #region constructor
        private readonly IUserService _userService;
        public AdminSideBarViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        #endregion
        public IViewComponentResult Invoke()
        {
            return View("AdminSideBar");
        }
    }
}
