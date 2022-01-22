using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.ViewComponents
{
    public class AdminFooterViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("AdminFooter");
        }
    }
}
