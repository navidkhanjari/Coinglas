using Academy.Domain.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Academy.Domain.Entities.Course
{
    public class CourseStatus : BaseEntity
    {
        #region Properties
        [Display(Name = "وضعیت دوره")]
        [Required]
        [MaxLength(150)]
        public string StatusTitle { get; set; }
        #endregion

        #region Relations
        public ICollection<Course> Courses { get; set; }
        #endregion
    }
}
