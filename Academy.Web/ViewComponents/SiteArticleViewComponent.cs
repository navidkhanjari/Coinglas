using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.ViewComponents
{
    public class SiteArticleViewComponent : ViewComponent
    {
        #region constructor
        private readonly IArticleService _articleService;

        public SiteArticleViewComponent(IArticleService articleService)
        {
            _articleService = articleService;
        }
        #endregion
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("SiteArticle",await _articleService.GetArticles());
        }
    }
}
