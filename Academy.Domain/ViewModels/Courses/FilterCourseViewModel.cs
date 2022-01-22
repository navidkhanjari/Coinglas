using Academy.Domain.Entities.Course;
using Academy.Domain.ViewModels.Paging;
using System;

namespace Academy.Domain.ViewModels.Courses
{
    public class FilterCourseViewModel:BasePaging<Course>
    {
        public string Title { get; set; }
        public DateTime? PublishDateFrom { get; set; }
        public DateTime? PublishDateTo { get; set; }
    }
}
