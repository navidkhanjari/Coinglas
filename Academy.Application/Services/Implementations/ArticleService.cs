using Academy.Application.Extensions;
using Academy.Application.FilePath;
using Academy.Application.Security;
using Academy.Application.Services.Interfaces;
using Academy.Application.Utils;
using Academy.Domain.Entities.Article;
using Academy.Domain.IRepositories;
using Academy.Domain.ViewModels.Article;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Academy.Application.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        #region constructor
        private readonly IArticleRepository _articleRepository;
        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }
        #endregion

        #region Filter Article
        public async Task<FilterArticleViewModel> FilterArticleAsync(FilterArticleViewModel filter)
        {
            return await _articleRepository.FilterArticleAsync(filter);
        }
        public async Task<FilterArticleViewModel> FilterDeletedArticleAsync(FilterArticleViewModel filter)
        {
            return await _articleRepository.FilterDeletedArticleAsync(filter);
        }
        #endregion

        #region  Article
        public async Task<CreateArticleResult> CreateArticle(CreateArticleViewModel createArticle)
        {
            try
            {
                var newArticle = new Article()
                {
                    Title = createArticle.Title.SanitizeText(),
                    CreateDate = DateTime.Now,
                    Description = createArticle.Description.SanitizeText(),
                    IsDelete = false,
                    ShortDescription = createArticle.ShortDescription.SanitizeText(),
                    Slug = createArticle.Slug.ToSlug(),
                    Visit = 0,
                    UserId = createArticle.UserId,
                    ImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(createArticle.ImageName.FileName)
                };
                // add Image To Server
                createArticle.ImageName.AddImageToServer(newArticle.ImageName, FilePaths.ArticleUploadPath, 150, 150, FilePaths.ArticleThumbUploadPath);

                await _articleRepository.AddArticleAsync(newArticle);
                await _articleRepository.SaveChangeAsync();

                return CreateArticleResult.Success;
            }
            catch (Exception)
            {
                return CreateArticleResult.Error;

                throw;
            }

        }

        public async Task<EditArticleResult> EditArticle(EditArticleViewModel editArticle)
        {
            try
            {
                var oldArticle = await _articleRepository.GetArticleById(editArticle.ArticleId);
                if (oldArticle == null)
                    return EditArticleResult.Error;

                //update article

                oldArticle.Title = editArticle.Title;
                oldArticle.Slug = editArticle.Slug.ToSlug();
                oldArticle.ShortDescription = editArticle.ShortDescription;
                oldArticle.Description = editArticle.Description;


                if (editArticle.NewImage != null)
                {
                    //delete old image
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), FilePaths.ArticleUploadPath, oldArticle.ImageName);
                    var imageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), FilePaths.ArticleThumbUploadPath, oldArticle.ImageName);
                    if (File.Exists(imagePath) && File.Exists(imageThumbPath))
                    {
                        File.Delete(imageThumbPath);
                        File.Delete(imagePath);
                    }
                    //add new image to server

                    oldArticle.ImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(editArticle.NewImage.FileName);
                    editArticle.NewImage.AddImageToServer(oldArticle.ImageName, FilePaths.ArticleUploadPath, 150, 150, FilePaths.ArticleThumbUploadPath);
                }

                await _articleRepository.EditArticle(oldArticle);
                return EditArticleResult.Success;
            }
            catch (Exception)
            {
                return EditArticleResult.Error;
                throw;
            }
        }
        public async Task<EditArticleViewModel> GetInformationArticleForEdit(long articleId)
        {
            return await _articleRepository.GetInformationArticle(articleId);
        }

        public async Task<DetailsArticleViewModel> GetDetailsArticle(long articleId)
        {
            return await _articleRepository.GetDetailsArticle(articleId);
        }

        public async Task<Article> GetArticleById(long articleId)
        {
            return await _articleRepository.GetArticleById(articleId);
        }

        public async Task DeleteArticle(long articleId)
        {
            var article = await _articleRepository.GetArticleById(articleId);
            article.IsDelete = true;
            await _articleRepository.SaveChangeAsync();
        }

        public async Task<List<ShowArticleViewModel>> GetArticles()
        {
            return await _articleRepository.GetArticles();
        }
        public async Task<FilterListArticlesViewModel> GetArticlesList(FilterListArticlesViewModel filter)
        {

            return await _articleRepository.GetArticlesList(filter);
        }

        public async Task<ShowArticleViewModel> GetArticleBySlug(string slug)
        {
            return await _articleRepository.GetArticleBySlug(slug);
        }
        #endregion
    }
}
