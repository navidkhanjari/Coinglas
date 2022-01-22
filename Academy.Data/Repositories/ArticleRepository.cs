using Academy.Data.Context;
using Academy.Domain.Entities.Article;
using Academy.Domain.IRepositories;
using Academy.Domain.ViewModels.Article;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Data.Repositories
{
    public class ArticleRepository:IArticleRepository
    {
        #region constructor
        private readonly ApplicationDbContext _context;
        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Filter Article
        public async Task<FilterArticleViewModel> FilterArticleAsync(FilterArticleViewModel filter)
        {
            var query = _context.Articles.AsQueryable();

            #region filter
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(r => EF.Functions.Like(r.Title, $"%{filter.Title}%"));
            }
            if (filter.PublishDateFrom != null)
            {
                query = query.Where(r => r.CreateDate >= filter.PublishDateFrom);
            }
            if(filter.PublishDateTo != null)
            {
                query = query.Where(r => r.CreateDate <= filter.PublishDateTo);
            }
            #endregion

            #region  Paging
            await filter.Build(await query.CountAsync()).SetEntities(query);
            #endregion
            return filter;
        }

        public async Task<FilterArticleViewModel> FilterDeletedArticleAsync(FilterArticleViewModel filter)
        {
            var query = _context.Articles.IgnoreQueryFilters().Where(r => r.IsDelete);

            #region filter
            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(r => EF.Functions.Like(r.Title, $"%{filter.Title}%"));
            }
            if (filter.PublishDateFrom != null)
            {
                query = query.Where(r => r.CreateDate >= filter.PublishDateFrom);
            }
            if (filter.PublishDateTo != null)
            {
                query = query.Where(r => r.CreateDate <= filter.PublishDateTo);
            }
            #endregion

            #region paging
            await filter.Build(await query.CountAsync()).SetEntities(query);
            #endregion

            return filter;
        }

        #endregion

        #region  Article
        public async Task<DetailsArticleViewModel> GetDetailsArticle(long articleId)
        {
            return await _context.Articles
                .Where(r => r.Id == articleId)
                .Select(r => new DetailsArticleViewModel()
                {
                    Id =r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    CreateDate = r.CreateDate,
                    ShortDescription = r.ShortDescription,
                    ImageName = r.ImageName,
                    Slug = r.Slug,
                    Visit = r.Visit,
                    UserId = r.UserId
                }).SingleOrDefaultAsync();
        }
        public async Task<Article> GetArticleById(long articleId)
        {
           return await _context.Articles.Where(p=>p.Id==articleId).SingleOrDefaultAsync();
        }
       
        public async Task AddArticleAsync(Article article)
        {
            await _context.Articles.AddAsync(article);
        }

        public async Task EditArticle(Article article)
        {
            _context.Articles.Update(article);
            await SaveChangeAsync();
        }

        public async Task<EditArticleViewModel> GetInformationArticle(long articleId)
        {
           return await _context.Articles.Where(r => r.Id == articleId)
                .Select(r => new EditArticleViewModel()
                {
                    ArticleId = r.Id,
                    Description = r.Description,
                    ImageName = r.ImageName,
                    ShortDescription = r.ShortDescription,
                    Title = r.Title,
                    Slug = r.Slug,

                }).SingleOrDefaultAsync();
        }
        public async Task<List<ShowArticleViewModel>> GetArticles()
        {
            return await _context.Articles
                .Select(r => new ShowArticleViewModel()
                {
                    Title = r.Title,
                    ShortDescription = r.ShortDescription,
                    CreateDate = r.CreateDate,
                    ImageName =r.ImageName,
                    Slug =r.Slug
                })
                .OrderByDescending(r=> r.CreateDate)
                .Take(3)
                .ToListAsync();
        }
        public async Task<FilterListArticlesViewModel> GetArticlesList(FilterListArticlesViewModel filter)
        {

            var query = _context.Articles.AsQueryable();

            #region filter
            if (!string.IsNullOrEmpty(filter.Search))
            {
                query = query.Where(r => EF.Functions.Like(r.Title, $"%{filter.Search}%"));
            }
            #endregion

            #region paging
            await filter.Build(await query.CountAsync()).SetEntities(query);
            #endregion

            return filter;        
        }
        public async Task<ShowArticleViewModel> GetArticleBySlug(string slug)
        {
            return await _context.Articles.Where(r => r.Slug == slug)
                .Select(r=> new ShowArticleViewModel()
                {
                    Title = r.Title,
                    ShortDescription = r.ShortDescription,
                    CreateDate = r.CreateDate,
                    ImageName = r.ImageName,
                    Description = r.Description,
                    Slug = r.Slug,
                    Visit = r.Visit,
                    UserId = r.UserId               
                })
                .SingleOrDefaultAsync();
        }
        #endregion

        #region SaveChanges
        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
