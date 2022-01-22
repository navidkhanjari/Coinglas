using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.Controllers
{

    public class SubscribeController : Controller
    {
        #region constructor
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public SubscribeController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        #endregion

        #region Orders
        [Authorize]
        [HttpGet("AddSubscribeToOrder/{Id}")]
        public async Task<IActionResult> BuySubscribe(long Id)
        {
            var userId = await _userService.GetUserIdByUserName(User.Identity.Name);
            long subId = await _orderService.AddSubscribeToOrder(userId, Id);
            return Redirect("/Account/MyOrders/ShowOrders/" + subId);
        }
        #endregion
    }
}
