using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.ViewModels.Courses
{
    public class CreateCourseEpisodeViewModel
    {
        public long CourseId { get; set; }

        [Display(Name ="نام قسمت")]
        public string EpisodeTitle { get; set; }

        [Display(Name = "زمان")]
        public TimeSpan EpisodeTime { get; set; }

        public IFormFile EpisodeFileName { get; set; }

        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }
    }
    public enum CreateEpisodeResult
    {
        Success,
        FileNameExist,
        Error
    }
}
