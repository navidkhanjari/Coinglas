using Academy.Domain.Entities.Course;
using Academy.Domain.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Academy.Domain.IRepositories
{
    public interface ICourseRepository
    {
        #region Group

        Task<List<CourseGroup>> GetAllGroupAsync();
        Task<List<SelectListItem>> GetGroupForManageCourseAsync();
        Task<List<SelectListItem>> GetSubGroupForManageCourseAsync(long groupId);
        Task<List<SelectListItem>> GetTeachersAsync();
        Task<List<SelectListItem>> GetLevelsAsync();
        Task<List<SelectListItem>> GetStatusesAsync();

        #endregion

        #region  Course
        Task AddCourseAsync(Course course);
        Task<Course> GetCourseByIdAsync(long Id);
        Task EditCourse(Course course);
        Task<EditCourseViewModel> GetInformationCourseForEdit(long Id);
        Task<List<ShowCourseListItemViewModel>> GetCourses();
        #endregion

        #region Filter Course
        Task<FilterCourseViewModel> FilterCoursesAsync(FilterCourseViewModel filterCourse);
        #endregion

        #region Course Episode
        Task AddCourseEpisodeAsync(CourseEpisode courseEpisode);
        Task<bool> CheckEpisodeFileNameIsExist(string fileName);
        Task<List<CourseEpisodeDetailViewModel>> GetEpisodesByCourseIdAsync(long courseId);
        Task<EditCourseEpisodeViewModel> GetEpisodesByIdForEditAsync(long episodeId);
        Task<CourseEpisode> GetEpisodeByIdAsync(long episodeId);
        Task EditCourseEpisode(CourseEpisode courseEpisode);

        #endregion

        #region SaveChanges
        Task SaveChangesAsync();
        #endregion
    }
}
