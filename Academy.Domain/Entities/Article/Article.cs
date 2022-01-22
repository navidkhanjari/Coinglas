using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Domain.Entities.Article
{
    public class Article : BaseEntity
    {
        #region Properties
        public long UserId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "وارد کردن {0}اجباری است")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }
        [Display(Name = "توضیح کوتاه")]
        [Required(ErrorMessage = "وارد کردن {0}اجباری است")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ShortDescription { get; set; }
        [Display(Name = "متن")]
        public string Description { get; set; }
        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "وارد کردن {0}اجباری است")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Slug { get; set; }
        public string ImageName { get; set; }
        [Display(Name = "بازدید")]

        public int Visit { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreateDate { get; set; }

        #endregion

        #region relations

        [ForeignKey("UserId")]
        public User User { get; set; }
        #endregion
    }
}
