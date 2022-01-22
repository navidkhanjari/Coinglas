using Academy.Application.Services.Interfaces;
using Academy.Domain.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.Controllers
{
    public class ArticlesController : SiteBaseController
    {
        #region constructor 
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        public ArticlesController(IArticleService articleService,IUserService userService)
        {
            _articleService = articleService;
            _userService = userService;
        }
        #endregion

        #region Index
        [HttpGet("articles")]
        public async Task<IActionResult> Index(FilterListArticlesViewModel filter)
        {
            var articles = await _articleService.GetArticlesList(filter);
           
            return View(articles);
        }
        #endregion

        #region Detail Article

        [HttpGet("article/news-article/{slug}")]
        public async Task<IActionResult> NewsArticle(string slug)
        {
            var article = await _articleService.GetArticleBySlug(slug);
            if (article == null)
                return NotFound();
            ViewData["Author"] = await _userService.GetUserNameById(article.UserId);
            ViewData["SiteUrl"]= $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}";
            return View(article);
        }
        #endregion
    }
}
