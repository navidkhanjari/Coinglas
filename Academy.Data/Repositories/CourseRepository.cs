using Academy.Data.Context;
using Academy.Domain.Entities.Course;
using Academy.Domain.IRepositories;
using Academy.Domain.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        #region costructor
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Group
        public async Task<List<CourseGroup>> GetAllGroupAsync()
        {
            return await _context.CourseGroups.ToListAsync();
        }

        public async Task<List<SelectListItem>> GetGroupForManageCourseAsync()
        {
            return await _context.CourseGroups.Where(g => g.ParentId == null)
                 .Select(g => new SelectListItem()
                 {
                     Text = g.GroupTitle,
                     Value = g.Id.ToString()
                 }).ToListAsync();
        }
        public async Task<List<SelectListItem>> GetSubGroupForManageCourseAsync(long groupId)
        {
            return await _context.CourseGroups.Where(g => g.ParentId == groupId)
               .Select(g => new SelectListItem()
               {
                   Text = g.GroupTitle,
                   Value = g.Id.ToString()
               }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetLevelsAsync()
        {
            return await _context.CourseLevels
                .Select(g => new SelectListItem()
                {
                    Text = g.LevelTitle,
                    Value = g.Id.ToString()
                }).ToListAsync();

        }

        public async Task<List<SelectListItem>> GetStatusesAsync()
        {
            return await _context.CourseStatuses
                 .Select(g => new SelectListItem()
                 {
                     Text = g.StatusTitle,
                     Value = g.Id.ToString()
                 }).ToListAsync();
        }

        public async Task<List<SelectListItem>> GetTeachersAsync()
        {
            return await _context.UserRoles
                .Include(r => r.User)
                .Where(g => g.RoleId == 1)
                .Select(g => new SelectListItem()
                {
                    Text = g.User.UserName,
                    Value = g.UserId.ToString()
                }).ToListAsync();
        }
        #endregion

        #region  Course
        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
        }
        public async Task<Course> GetCourseByIdAsync(long Id)
        {
            return await _context.Courses.Where(r => r.Id == Id).SingleOrDefaultAsync();
        }
        public async Task EditCourse(Course course)
        {
            _context.Update(course);
            await SaveChangesAsync();
        }

        public async Task<EditCourseViewModel> GetInformationCourseForEdit(long Id)
        {
            return await _context.Courses.Where(r => r.Id == Id)
                .Select(r => new EditCourseViewModel()
                {
                    Id = r.Id,
                    CourseDescription = r.CourseDescription,
                    CourseImageName = r.CourseImageName,
                    CoursePrice = r.CoursePrice,
                    CourseTitle = r.CourseTitle,
                    GroupId = r.GroupId,
                    LevelId = r.LevelId,
                    StatusId = r.StatusId,
                    SubGroup = r.SubGroup,
                    Tags = r.Tags,
                    TeacherId = r.TeacherId,
                    DemoFileName = r.DemoFileName,

                }).SingleOrDefaultAsync();
        }
        public async Task<List<ShowCourseListItemViewModel>> GetCourses()
        {
            return await _context.Courses               
                .Select(r=> new ShowCourseListItemViewModel 
                {  
                    Id =r.Id,
                    Title = r.CourseTitle,
                    Price =r.CoursePrice,
                    ImageName = r.CourseImageName
                    
                }).Take(6).ToListAsync();
        }

        #endregion

        #region Filter Course
        public async Task<FilterCourseViewModel> FilterCoursesAsync(FilterCourseViewModel filterCourse)
        {
            var query = _context.Courses.AsQueryable();

            #region filter 
            if (!string.IsNullOrEmpty(filterCourse.Title))
            {
                query = _context.Courses.Where(r => EF.Functions.Like(r.CourseTitle, $"{filterCourse.Title}"));
            }
            if (filterCourse.PublishDateFrom != null)
            {
                query = query.Where(r => r.CreateDate >= filterCourse.PublishDateFrom);
            }
            if (filterCourse.PublishDateTo != null)
            {
                query = query.Where(r => r.CreateDate <= filterCourse.PublishDateTo);
            }
            #endregion

            #region paging
            await filterCourse.Build(await query.CountAsync()).SetEntities(query);
            #endregion

            return filterCourse;
        }
        #endregion

        #region Course Episode
        public async Task AddCourseEpisodeAsync(CourseEpisode courseEpisode)
        {
            await _context.CourseEpisodes.AddAsync(courseEpisode);
        }
        public async Task<bool> CheckEpisodeFileNameIsExist(string fileName)
        {
            return await _context.CourseEpisodes.AnyAsync(r => r.EpisodeFileName == fileName);


        }
        public async Task<List<CourseEpisodeDetailViewModel>> GetEpisodesByCourseIdAsync(long courseId)
        {
            return await _context.CourseEpisodes.Where(r => r.CourseId == courseId)
                .Select(r => new CourseEpisodeDetailViewModel()
                {
                    Id = r.Id,
                    CourseId = r.CourseId,
                    EpisodeTitle = r.EpisodeTitle,
                    EpisodeTime = r.EpisodeTime,
                    EpisodeFileName = r.EpisodeFileName,
                    IsFree = r.IsFree
                })
                .ToListAsync();
        }
        public async Task<EditCourseEpisodeViewModel> GetEpisodesByIdForEditAsync(long episodeId)
        {
            return await _context.CourseEpisodes.Where(r => r.Id == episodeId)
                 .Select(r => new EditCourseEpisodeViewModel()
                 {
                     Id = r.Id,
                     EpisodeTime = r.EpisodeTime,
                     EpisodeFileName = r.EpisodeFileName,
                     EpisodeTitle = r.EpisodeTitle,
                     IsFree = r.IsFree,
                 }).SingleOrDefaultAsync();
        }
        public async Task<CourseEpisode> GetEpisodeByIdAsync(long episodeId)
        {
            return await _context.CourseEpisodes.FindAsync(episodeId);
        }
        public async Task EditCourseEpisode(CourseEpisode courseEpisode)
        {
            _context.CourseEpisodes.Update(courseEpisode);
            await SaveChangesAsync();
        }
        #endregion

        #region Save Changes
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }

}
