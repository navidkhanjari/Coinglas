
using System.ComponentModel.DataAnnotations;

namespace Academy.Domain.ViewModels.Account
{
   public class RegisterViewModel
    {
        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string Email { get; set; }

        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [RegularExpression((@"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$"), ErrorMessage = "شماره تماس وارد شده معتبر نمی باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام کاربری")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,20}$", ErrorMessage = "کلمه عبور باید شامل حرف و عدد باشد")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage ="{0} نمیتواند کمتر از 6 کاراکتر داشته باشد ")]
        public string Password { get; set; }

        [Display(Name = " تکرار کلمه عبور ")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="کلمه های عبور مغایرت دارند")]
        public string ConfirmPassword { get; set; }
      
    }

    public enum RegisterUserResult
    {
        Success,
        UserExist,
        reCaptchaFalse
    }
}
