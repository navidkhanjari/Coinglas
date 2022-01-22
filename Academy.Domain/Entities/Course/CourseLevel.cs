using Academy.Domain.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Academy.Domain.Entities.Course
{
    public class CourseLevel : BaseEntity
    {
        #region Properties
        [Display(Name = "سطح دوره")]
        [Required]
        [MaxLength(150)]
        public string LevelTitle { get; set; }
        #endregion

        #region Relations
        public ICollection<Course> Courses { get; set; }
        #endregion
    }
}
