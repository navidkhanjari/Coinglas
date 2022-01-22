using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.Entities.Course
{
    public class UserCourse : BaseEntity
    {
        #region Properties
        public long CourseId { get; set; }
        public long UserId { get; set; }
        #endregion

        #region relations

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        #endregion
    }
}
