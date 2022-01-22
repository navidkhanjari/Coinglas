using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.Areas.Account.Controllers
{
    public class MyOrdersController : AccountBaseController
    {
        #region constructor
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public MyOrdersController(IOrderService orderService, IUserService userService)
        {
            _userService = userService;
            _orderService = orderService;
        }
        #endregion

        #region Index
        [HttpGet("Account/MyOrders")]
        public async Task<IActionResult> Index()
        {
            var userId = await _userService.GetUserIdByUserName(User.Identity.Name);
            return View(await _orderService.GetUserOrder(userId));
        }
        #endregion

        #region Orders
        [HttpGet("Account/MyOrders/ShowOrders/{Id}")]
        [HttpGet("MyOrders/ShowOrders/{Id}")]
        public async Task<IActionResult> ShowOrder(long Id,string type ="")
        {
            var userId = await _userService.GetUserIdByUserName(User.Identity.Name);
            var order = await _orderService.GetOrderForUserPanel(userId, Id);

            if (order == null)
            {
                return NotFound();
            }

            ViewBag.DiscountTypes = type;
            return View(order);
        }

        [Route("Account/MyOrders/FinallyOrder/{Id}")]
        public async Task<IActionResult> FinallyOrder(long Id)
        {
            var userId = await _userService.GetUserIdByUserName(User.Identity.Name);

            if(await _orderService.FinalyOrder(userId, Id))
            {

                return Redirect("/Account/Wallet/?finaly=true");
            }

            return BadRequest();
        }

        #endregion

        #region Discount
        [HttpPost("Account/MyOrders/UseDiscount")]
        public async Task<IActionResult> UseDiscount(long orderId ,string code)
        {
            var types = await _orderService.UseDiscount(orderId,code.Trim().ToLower());

            return Redirect("/MyOrders/ShowOrders/" + orderId + "?type=" + types.ToString());
        }
        #endregion
    }
}
