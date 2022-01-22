using System.IO;

namespace Academy.Application.FilePath
{
    public static class FilePaths
    {
     

        #region Article Images

        public static readonly string ArticlePath = "/images/article/origin/";
        public static readonly string ArticleUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/article/origin/");
        public static readonly string ArticleThumbPath = "/images/article/thumb/";
        public static readonly string ArticleThumbUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/article/thumb/");

        public static string UploadImagePathServer { get; set; }

        #endregion

        #region Course Images
        public static readonly string CoursePasth = "/images/course/origin/";
        public static readonly string CourseUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course/origin/");
        public static readonly string CourseThumbPath = "/images/course/thumb/";
        public static readonly string CourseThumbUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course/thumb/");
        #endregion

        #region Course demo

        public static readonly string CourseDemoPasth = "/images/course/demo/";
        public static readonly string CourseDemoUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/course/demo/");

        #endregion

        #region MyImages Ckeditor
        public static readonly string MyImagesPath = "/MyImages/";
        public static readonly string MyImagesUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/MyImages/");

        #endregion

        #region Course Episode Path
        public static readonly string CourseEpisodePath = "/courseFiles/";
        public static readonly string CourseEpisodeUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/courseFiles/");

        #endregion
    }
}
