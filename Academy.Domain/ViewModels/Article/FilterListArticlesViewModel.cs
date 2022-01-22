using Academy.Domain.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.ViewModels.Article
{
    public class FilterListArticlesViewModel:BasePaging<Entities.Article.Article>
    {
        public string Search { get; set; }
    }
}
