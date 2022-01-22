using Academy.Domain.Entities.Common;
using Academy.Domain.Entities.Course;
using Academy.Domain.Entities.Subscribe;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Academy.Domain.Entities.Account
{
    public class User : BaseEntity
    {
        #region Properties
        [Display(Name = "نام کاربری")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Password { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [EmailAddress(ErrorMessage ="ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }
        [Display(Name ="شماره تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [RegularExpression((@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$"),ErrorMessage ="شماره تماس وارد شده معتبر نمی باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "کد فعالسازی")]
        public string ActivationCode { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
        public bool IsDelete { get; set; }
        #endregion

        #region Relations
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<Wallet.Wallet> Wallets { get; set; }
        public ICollection<Article.Article> Articles { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }
        public ICollection<UserDiscountCode> UserDiscountCodes { get; set; }
        public ICollection<UserSubscribes> UserSubscribes { get; set; }

        #endregion
    }
}
