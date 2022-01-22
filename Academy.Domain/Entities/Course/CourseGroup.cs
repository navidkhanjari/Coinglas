using Academy.Domain.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Academy.Domain.Entities.Course
{
    public class CourseGroup : BaseEntity
    {
        #region properties
        [Display(Name = "عنوان گروه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string GroupTitle { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsDelete { get; set; }

        [Display(Name = "گروه اصلی")]
        public long? ParentId { get; set; }
        #endregion

        #region relations
        [ForeignKey("ParentId")]
        public ICollection<CourseGroup> CourseGroups { get; set; }

        [InverseProperty("CourseGroup")]
        public ICollection<Course> Courses { get; set; }

        [InverseProperty("Group")]
        public ICollection<Course> SubGroup { get; set; }
        #endregion
    }
}
