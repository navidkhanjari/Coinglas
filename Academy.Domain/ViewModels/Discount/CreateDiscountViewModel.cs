using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.ViewModels.Discount
{
    public class CreateDiscountViewModel
    {
        [Display(Name = "کد تخفیف")]
        [Required(ErrorMessage = "وارد کردن {0}اجباری است")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Code { get; set; }

        [Display(Name = "درصد تخفیف")]
        [Required(ErrorMessage = "وارد کردن {0}اجباری است")]
        public int DiscountPercent { get; set; }

        [Display(Name = "تعداد")]
        public int UsableDiscount { get; set; }

        [Display(Name = "تاریخ شروع")]
        public DateTime StartDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        public DateTime EndDate { get; set; }
    }
    public enum CreateDiscountResult
    {
        Exist,
        success,
       
    }
}
