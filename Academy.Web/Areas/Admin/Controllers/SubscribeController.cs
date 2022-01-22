using Academy.Application.Security;
using Academy.Application.Services.Interfaces;
using Academy.Domain.Entities.Subscribe;
using Academy.Domain.ViewModels.Subscribes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.Areas.Admin.Controllers
{
    public class SubscribeController : AdminBaseController
    {
        #region counstructor
        private readonly ISubscribeService _subscribeService;
        public SubscribeController(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }
        #endregion

        #region index
        [PermissionChecker(20002)]
        [HttpGet("Admin/Subscribes")]
        public async Task<IActionResult> Index()
        {
            return View("Index", await _subscribeService.GetSubscribes());
        }
        #endregion

        #region Create
        [PermissionChecker(20002)]
        [HttpGet("Admin/Subscribes/Create")]
        public IActionResult Create()
        {
            return View("CreateSub");
        }
        [HttpPost("Admin/Subscribes/Create"), ValidateAntiForgeryToken]
        [PermissionChecker(20002)]
        public async Task<IActionResult> Create(Subscribe subscribe)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateSub",subscribe);
            }
            await _subscribeService.CreateSubscribe(subscribe);

            return RedirectToAction("Index");
        }
        #endregion

        #region Edit
        [PermissionChecker(20002)]
        [HttpGet("Admin/Subscribes/Edit/{Id}")]
        public async Task<IActionResult> Edit(long Id)
        {
            var Subscribe = await _subscribeService.GetSubscribeById(Id);
            if (Subscribe == null)
                return NotFound();

            return View("EditSub",Subscribe);
        }
        [PermissionChecker(20002)]
        [HttpPost("Admin/Subscribes/Edit/{Id}"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Subscribe subscribe , long Id)
        {
            if (!ModelState.IsValid)
                return View("EditSub", subscribe);

            await _subscribeService.EditSubscribe(subscribe);

            return RedirectToAction("Index");
        }
        #endregion

        #region Show Buyers
        [PermissionChecker(20002)]
        [HttpGet("Admin/Subscribes/Buyers/{Id}")]
        public async Task<IActionResult> Buyers(long Id)
        {
            var model = await _subscribeService.GetSubscribeBuyers(Id);
            if (model == null)
                return NotFound();

            return View(model); 
        }
        #endregion
    }
}
