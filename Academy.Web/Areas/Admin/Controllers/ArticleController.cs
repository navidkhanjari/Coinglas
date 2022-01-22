using Academy.Application.Security;
using Academy.Application.Services.Interfaces;
using Academy.Application.Utils;
using Academy.Application.Utils.Convertors;
using Academy.Domain.Entities.Article;
using Academy.Domain.ViewModels.Article;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.Areas.Admin.Controllers
{
    public class ArticleController : AdminBaseController
    {
        #region constructor
        private readonly IArticleService _articleService;
        private readonly IUserService _userService;
        public ArticleController(IArticleService articleService, IUserService userService)
        {
            _articleService = articleService;
            _userService = userService;
        }
        #endregion

        #region filter article
        [PermissionChecker(3)]
        [HttpGet("Admin/Article")]
        public async Task<IActionResult> Index(FilterArticleViewModel filterArticle)
        {
            return View(await _articleService.FilterArticleAsync(filterArticle));
        }
        [PermissionChecker(3)]
        [HttpGet("Admin/Article/DeletedArticle")]
        public async Task<IActionResult> DeletedArticle(FilterArticleViewModel filterArticle)
        {
            return View(await _articleService.FilterDeletedArticleAsync(filterArticle));
        }
        #endregion

        #region Create
        [PermissionChecker(9)]
        [HttpGet("Admin/Article/Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["UserId"] = await _userService.GetUserIdByUserName(User.Identity.Name);

            return View();
        }


        [HttpPost("Admin/Article/Create"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateArticleViewModel createArticle)
        {
            if (!ModelState.IsValid)
                return View(createArticle);

            var res = await _articleService.CreateArticle(createArticle);
            switch (res)
            {
                case CreateArticleResult.Success:
                    return RedirectToAction("Index");
                case CreateArticleResult.Error:
                    ModelState.AddModelError("Title", "خطایی در انجام عملیات رخ داد!");
                    break;
            }

            return View(createArticle);

        }
        #endregion

        #region Edit
        [PermissionChecker(10)]
        [HttpGet("Admin/Article/Edit/{Id}")]
        public async Task<IActionResult> Edit(long Id)
        {
            var article = await _articleService.GetInformationArticleForEdit(Id);
            if (article == null)
                return NotFound(Id);

            return View(article);
        }

        [HttpPost("Admin/Article/Edit/{Id}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditArticleViewModel editArticle)
        {
            if (!ModelState.IsValid)
                return View(editArticle);

            var res = await _articleService.EditArticle(editArticle);
            switch (res)
            {
                case EditArticleResult.Success:
                    return RedirectToAction("Index");
                case EditArticleResult.Error:
                    ModelState.AddModelError("Title", "خطایی در انجام عملیات رخ داد!");
                    break;
            }

            return View(editArticle);

        }
        #endregion

        #region Delete
        [PermissionChecker(11)]
        [HttpGet("Admin/Article/Delete/{Id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            //TODO: take it to the service
            var article = await _articleService.GetArticleById(Id);
            if (article == null)
                return NotFound();
            var model = new DetailsArticleViewModel()
            {
                Id = article.Id,
                UserId = article.UserId,
                Description = article.Description,
                CreateDate = article.CreateDate,
                IsDelete = article.IsDelete,
                ShortDescription = article.ShortDescription,
                Slug = article.Slug,
                Title = article.Title,
                Visit = article.Visit,
                ImageName = article.ImageName
            };
            ViewData["UserName"] = await _userService.GetUserNameById(article.UserId);

            return View(model);

        }
        [HttpPost("Admin/Article/Delete/{Id}"),ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long Id)
        {
            await _articleService.DeleteArticle(Id);
            return RedirectToAction("Index");
        }
        #endregion

        #region Detail
        [PermissionChecker(3)]
        [HttpGet("Admin/Article/Detail/{Id}")]
        public async Task<IActionResult> Detail(long Id)
        {
            var article = await _articleService.GetDetailsArticle(Id);
            if (article == null)
                return NotFound();
            ViewData["UserName"] = await _userService.GetUserNameById(article.UserId);
            return View(article);
        }
        #endregion

  
    }
}
