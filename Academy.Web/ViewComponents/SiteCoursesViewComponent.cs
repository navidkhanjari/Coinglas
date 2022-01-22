using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.ViewComponents
{
    public class SiteCoursesViewComponent : ViewComponent
    {
        #region constructor
        private readonly ICourseService _courseService;
        public SiteCoursesViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }
        #endregion
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteCourses",await _courseService.GetCourses());
        }
    }
}
