using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.Controllers
{
    public class CourseController : SiteBaseController
    {
        #region constructor
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public CourseController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        #endregion

        #region Index

        [HttpGet("Courses")]
        public async Task<IActionResult> Courses()
        {
            return View();
        }
        #endregion

        #region Orders
        [Authorize]
        [HttpGet("AddCourseToOrder/{Id}")]
        public async Task<IActionResult> BuyCourse(long Id)
        {
            var userId = await _userService.GetUserIdByUserName(User.Identity.Name);
            var courseId = await _orderService.AddCourseToOrder(userId, Id);
            return Redirect("/Account/MyOrders/ShowOrders/" + courseId);
        }
        #endregion

    }
}
