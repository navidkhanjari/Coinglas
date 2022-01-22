using Academy.Domain.Entities.Article;
using Academy.Domain.ViewModels.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.IRepositories
{
    public interface IArticleRepository
    {
        #region filter Articles 
        Task<FilterArticleViewModel> FilterArticleAsync(FilterArticleViewModel filter);
        Task<FilterArticleViewModel> FilterDeletedArticleAsync(FilterArticleViewModel filter);
        #endregion

        #region Article
        Task<DetailsArticleViewModel> GetDetailsArticle(long articleId);
        Task<Article> GetArticleById(long articleId);
        Task AddArticleAsync(Article article);
        Task EditArticle(Article article);
        Task<EditArticleViewModel> GetInformationArticle(long articleId);
        Task<List<ShowArticleViewModel>> GetArticles();
        Task<FilterListArticlesViewModel> GetArticlesList(FilterListArticlesViewModel filter);
        Task<ShowArticleViewModel> GetArticleBySlug(string slug);
       
        #endregion

        #region SaveChanges
        Task SaveChangeAsync();
        #endregion
    }
}
