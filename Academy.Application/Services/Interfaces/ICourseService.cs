using Academy.Domain.Entities.Course;
using Academy.Domain.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Academy.Application.Services.Interfaces
{
    public interface ICourseService
    {
        #region Group

        Task<List<CourseGroup>> GetAllGroup();
        Task<List<SelectListItem>> GetGroupForManageCourse();
        Task<List<SelectListItem>> GetSubGroupForManageCourse(long groupId);
        Task<List<SelectListItem>> GetTeachers();
        Task<List<SelectListItem>> GetLevels();
        Task<List<SelectListItem>> GetStatuses();

        #endregion

        #region Course
        Task<CreateCourseResult> AddCourse(CreateCourseViewModel createCourse);
        Task<EditCourseResult> EditCourse(EditCourseViewModel editCourse);
        Task<Course> GetCourseById(long Id);
        Task<EditCourseViewModel> GetInformationCourseForEdit(long Id);
        Task<List<ShowCourseListItemViewModel>> GetCourses();

        #endregion

        #region Filter Course
        Task<FilterCourseViewModel> FilterCourses(FilterCourseViewModel filterCourse);
        #endregion

        #region Course Episode
        Task<CreateEpisodeResult> CreateCourseEpisode(CreateCourseEpisodeViewModel createCourse);
        Task<List<CourseEpisodeDetailViewModel>> GetEpisodesByCourseId(long courseId);
        Task<EditCourseEpisodeViewModel> GetEpisodesByIdForEdit(long episodeId);
        Task<EditCourseEpisodeResult> EditCourseEpisode(EditCourseEpisodeViewModel editCourseEpisode);
        #endregion

        #region SaveChanges
        Task SaveChanges();
        #endregion
    }
}
