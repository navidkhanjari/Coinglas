
using System.ComponentModel.DataAnnotations;


namespace Academy.Domain.ViewModels.Account
{
   public class ForgotPasswordViewModel
    {
        [Display(Name = "ایمیل")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]

        public string Email { get; set; }
    }

    public enum ForgotPasswordResult
    {
        NotFound,
        Success,
        reCaptchaFalse
    }
}
