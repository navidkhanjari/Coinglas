using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.ViewModels.Courses
{
   public class EditCourseViewModel
    {
        public long Id { get; set; }
        [Display(Name = "گروه اصلی")]
        public long GroupId { get; set; }

        [Display(Name = "زیر گروه")]
        public long? SubGroup { get; set; }
        [Display(Name = "مدرس دوره")]
        [Required]
        public long TeacherId { get; set; }

        [Required]
        [Display(Name = "وضعیت دوره")]
        public long StatusId { get; set; }

        [Display(Name = "سطح دوره")]
        [Required]
        public long LevelId { get; set; }

        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CourseTitle { get; set; }

        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CourseDescription { get; set; }

        [Display(Name = "توضیحات کوتاه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string ShortDescription { get; set; }

        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CoursePrice { get; set; }

        [Display(Name = "کلمات کلیدی دوره")]
        [MaxLength(600)]
        public string Tags { get; set; }

        [Display(Name = "تصویر فعلی دوره")]
        public string CourseImageName { get; set; }

        [Display(Name = "دمو  فعلی دوره")]
        public string DemoFileName { get; set; }

        [Display(Name = "تصویر دوره")]
        public IFormFile newCourseImageName { get; set; }

        [Display(Name = "دمو دوره")]
        public IFormFile newDemoFileName { get; set; }

    }
    public enum EditCourseResult
    {
        success,
        error
    }
}
