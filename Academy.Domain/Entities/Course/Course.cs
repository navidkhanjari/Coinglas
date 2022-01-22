using Academy.Domain.Entities.Common;
using Academy.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Academy.Domain.Entities.Course
{
    public class Course : BaseEntity
    {

        #region properties
        [Required]
        public long GroupId { get; set; }

        public long? SubGroup { get; set; }

        [Required]
        public long TeacherId { get; set; }

        [Required]
        public long StatusId { get; set; }

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

        public bool IsDelete { get; set; }

        [MaxLength(600)]
        public string Tags { get; set; }

        [MaxLength(50)]
        public string CourseImageName { get; set; }

        [MaxLength(100)]
        public string DemoFileName { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }
        #endregion



        #region Relations

        [ForeignKey("TeacherId")]
        public Account.User User { get; set; }

        [ForeignKey("GroupId")]
        public CourseGroup CourseGroup { get; set; }

        [ForeignKey("SubGroup")]
        public CourseGroup Group { get; set; }

        [ForeignKey("StatusId")]
        public CourseStatus CourseStatus { get; set; }

        [ForeignKey("LevelId")]
        public CourseLevel CourseLevel { get; set; }

        public ICollection<CourseEpisode> CourseEpisodes { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public ICollection<UserCourse> UserCourses { get; set; }

        #endregion
    }
}
