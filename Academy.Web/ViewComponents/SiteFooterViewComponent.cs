using Microsoft.AspNetCore.Mvc;

namespace Academy.Web.ViewComponents
{
    public class SiteFooterViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("SiteFooter");
        }
    }
}
