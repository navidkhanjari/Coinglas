using Academy.Application.Security;
using Academy.Application.Services.Interfaces;
using Academy.Domain.ViewModels.Discount;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.Areas.Admin.Controllers
{
    public class DiscountController : AdminBaseController
    {
        #region Constructor
        private readonly IOrderService _orderService;
        public DiscountController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        #endregion

        #region Index
        [PermissionChecker(20003)]
        [HttpGet("Admin/Discount")]
        [HttpGet("Admin/Discount/Index")]
        public async Task<IActionResult> Index(FilterDiscountViewModel filter)
        {
            return View(await _orderService.GetDiscounts(filter));
        }
        #endregion

        #region Create
        [PermissionChecker(20003)]
        [HttpGet("Admin/Discount/Create")]
        public IActionResult Create()
        {
            return View();
        }
     
        [HttpPost("Admin/Discount/Create")]
        public async Task<IActionResult> Create(CreateDiscountViewModel discount)
        {
            if (!ModelState.IsValid)
                return View(discount);

            var res = await _orderService.CreateDiscount(discount);
            switch (res)
            {
                case CreateDiscountResult.Exist:
                    ModelState.AddModelError("Code", "کد تکراری است!");
                    break;
                case CreateDiscountResult.success:
                    return RedirectToAction(nameof(Index));

            }
            return View(discount);
        }
        #endregion

        #region Edit
        [PermissionChecker(20003)]
        [HttpGet("Admin/Discount/Edit/{Id}")]
        public async Task<IActionResult> Edit(long Id)
        {
            var discount = await _orderService.GetDiscountByIdForEdit(Id);

            if (discount == null)
                return NotFound();

            return View(discount);
        }

    
        [HttpPost("Admin/Discount/Edit/{Id}")]
        public async Task<IActionResult> Edit(EditDiscountViewModel editDiscount,long Id)
        {
            if (!ModelState.IsValid)
                return View(editDiscount);

            var res = await _orderService.EditDiscount(editDiscount);
            switch (res)
            {
                case EditDiscountResult.success:
                    return RedirectToAction(nameof(Index));
                case EditDiscountResult.error:
                    ModelState.AddModelError("Code", "خطایی رخ داد!");
                    break;
            }

            return View(editDiscount);
        }
        #endregion
    }
}
