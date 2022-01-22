using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.ViewComponents
{
    public class SiteSubViewComponent:ViewComponent
    {
        #region constructor
        private readonly ISubscribeService _subscribeService;
        public SiteSubViewComponent(ISubscribeService subscribeService)
        {
            _subscribeService = subscribeService;
        }
        #endregion
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteSub",await _subscribeService.GetSubscribeForShow());
        }
    }
}
