using Academy.Domain.ViewModels.Paging;
using System;


namespace Academy.Domain.ViewModels.Article
{
   public class FilterArticleViewModel:BasePaging<Entities.Article.Article>
    {
        public string Title { get; set; }
        public DateTime? PublishDateFrom { get; set; }
        public DateTime? PublishDateTo { get; set; }
    }
}
