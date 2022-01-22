using Academy.Domain.Entities.Article;
using Academy.Domain.ViewModels.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.Interfaces
{
    public interface IArticleService
    {
        #region filter Articles 
        Task<FilterArticleViewModel> FilterArticleAsync(FilterArticleViewModel filter);
        Task<FilterArticleViewModel> FilterDeletedArticleAsync(FilterArticleViewModel filter);
        #endregion

        #region Article
        Task<DetailsArticleViewModel> GetDetailsArticle(long articleId);
        Task<CreateArticleResult> CreateArticle(CreateArticleViewModel createArticle);
        Task<EditArticleResult> EditArticle(EditArticleViewModel editArticle);
        Task<EditArticleViewModel> GetInformationArticleForEdit(long articleId);
        Task<Article> GetArticleById(long articleId);
        Task<ShowArticleViewModel> GetArticleBySlug(string slug);
        Task DeleteArticle(long articleId);
        Task<List<ShowArticleViewModel>> GetArticles();
        Task<FilterListArticlesViewModel> GetArticlesList(FilterListArticlesViewModel filter);

        #endregion
    }
}
