using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.ViewModels.Article
{
    public class DetailsArticleViewModel
    {
        public long UserId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string ImageName { get; set; }  
        public int Visit { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
