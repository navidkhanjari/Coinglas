using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.ViewModels.Article
{
   public class ShowArticleViewModel
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }
        public int Visit { get; set; }
        public long UserId { get; set; }
        public string Slug { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
