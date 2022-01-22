using System;
using System.ComponentModel.DataAnnotations;

namespace Academy.Domain.ViewModels.Courses
{
    public class CourseEpisodeDetailViewModel
    {
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string EpisodeTitle { get; set; }

        [Display(Name = "زمان")]
        public TimeSpan EpisodeTime { get; set; }

        public string EpisodeFileName { get; set; }

        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }
    }
}
