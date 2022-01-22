using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Academy.Web.ViewComponents
{
    public class SiteHeaderViewComponent:ViewComponent
    {
     
  
        public  IViewComponentResult Invoke()
        {
            
            return View("SiteHeader");
        }
    }
}
