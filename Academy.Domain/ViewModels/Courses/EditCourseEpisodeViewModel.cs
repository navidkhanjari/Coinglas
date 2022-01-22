using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Academy.Domain.ViewModels.Courses
{
   public class EditCourseEpisodeViewModel
    {
        public long Id { get; set; }
        [Display(Name = "نام")]
        public string EpisodeTitle { get; set; }
        [Display(Name = "زمان")]
        public TimeSpan EpisodeTime { get; set; }
        public string EpisodeFileName { get; set; }
        [Display(Name = "فایل جدید")]
        public IFormFile newEpisodeFileName { get; set; }

        public bool IsFree { get; set; }
    }
    public enum EditCourseEpisodeResult
    {
        Success,
        Error,
        FileNameExist
    }
}
