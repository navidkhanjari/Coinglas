using Academy.Application.Services.Interfaces;
using Academy.Domain.Entities.Course;
using Academy.Domain.IRepositories;
using Academy.Domain.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using Academy.Application.Extensions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Academy.Application.FilePath;

namespace Academy.Application.Services.Implementations
{
   public class CourseService:ICourseService
    {
        #region costructor
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }
        #endregion

        #region group
        public async Task<List<CourseGroup>> GetAllGroup()
        {
            return await _courseRepository.GetAllGroupAsync();
        }

        public async Task<List<SelectListItem>> GetGroupForManageCourse()
        {
            return await _courseRepository.GetGroupForManageCourseAsync();
        }

        public async Task<List<SelectListItem>> GetLevels()
        {
            return await _courseRepository.GetLevelsAsync();
        }

        public async Task<List<SelectListItem>> GetStatuses()
        {
            return await _courseRepository.GetStatusesAsync();
        }

        public async Task<List<SelectListItem>> GetSubGroupForManageCourse(long groupId)
        {
            return await _courseRepository.GetSubGroupForManageCourseAsync(groupId);
        }

        public async Task<List<SelectListItem>> GetTeachers()
        {
            return await _courseRepository.GetTeachersAsync();
        }
        #endregion

        #region Course
        public async Task<CreateCourseResult> AddCourse(CreateCourseViewModel createCourse)
        {
            try
            {
                var newCourse = new Course()
                {
                    GroupId = createCourse.GroupId,
                    SubGroup = createCourse.SubGroup,
                    TeacherId = createCourse.TeacherId,
                    StatusId = createCourse.StatusId,
                    LevelId = createCourse.LevelId,
                    CourseTitle = createCourse.CourseTitle,
                    CourseDescription = createCourse.CourseDescription,
                    ShortDescription =createCourse.ShortDescription,
                    CoursePrice = createCourse.CoursePrice,
                    Tags = createCourse.Tags,
                    CourseImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(createCourse.CourseImageName.FileName),
                    DemoFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(createCourse.DemoFileName.FileName),
                    CreateDate = DateTime.Now
                };
                //add image to server 
                createCourse.CourseImageName.AddImageToServer(newCourse.CourseImageName, FilePaths.CourseUploadPath, 150, 150, FilePaths.CourseThumbUploadPath);

                //Add Demo File 
                createCourse.DemoFileName.AddFileToServer(newCourse.DemoFileName, FilePaths.CourseDemoUploadPath); 

                await _courseRepository.AddCourseAsync(newCourse);
                await _courseRepository.SaveChangesAsync();
                
                return CreateCourseResult.Success;
            }
            catch (Exception)
            {
                return CreateCourseResult.Error;
                throw;
            }
        }
        public async Task<EditCourseResult> EditCourse(EditCourseViewModel editCourse)
        {
            try
            {
                var course = await _courseRepository.GetCourseByIdAsync(editCourse.Id);
                if (course == null)
                    return EditCourseResult.error;

                //update course

                course.GroupId = editCourse.GroupId;
                course.LevelId = editCourse.LevelId;
                course.StatusId = editCourse.StatusId;
                course.SubGroup = editCourse.SubGroup;
                course.CourseTitle = editCourse.CourseTitle;
                course.CourseDescription = editCourse.CourseDescription;
                course.CoursePrice = editCourse.CoursePrice;
                course.TeacherId = editCourse.TeacherId;
                course.Tags = editCourse.Tags;
                course.ShortDescription = editCourse.ShortDescription;
                
                // Image

                if (editCourse.newCourseImageName != null)
                {
                    //delete old image
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), FilePaths.CourseUploadPath, course.CourseImageName);
                    var imageThumbPath = Path.Combine(Directory.GetCurrentDirectory(), FilePaths.CourseThumbUploadPath, course.CourseImageName);
                    if (File.Exists(imagePath) && File.Exists(imageThumbPath))
                    {
                        File.Delete(imageThumbPath);
                        File.Delete(imagePath);
                    }
                    //add new image to server

                    course.CourseImageName = Guid.NewGuid().ToString("N") + Path.GetExtension(editCourse.newCourseImageName.FileName);
                    editCourse.newCourseImageName.AddImageToServer(course.CourseImageName, FilePaths.CourseUploadPath, 150, 150, FilePaths.CourseThumbUploadPath);
                }

                // Demo File
                if(editCourse.newDemoFileName != null)
                {
                    //delete old demo
                    var DemoPath = Path.Combine(Directory.GetCurrentDirectory(), FilePaths.CourseDemoUploadPath, course.DemoFileName);
                    if (File.Exists(DemoPath))
                    {
                        File.Delete(DemoPath);
                    }
                    //add new Demo To server
                    course.DemoFileName = Guid.NewGuid().ToString("N") + Path.GetFileName(editCourse.newDemoFileName.FileName);
                    editCourse.newDemoFileName.AddFileToServer(editCourse.DemoFileName, FilePaths.CourseDemoUploadPath);
                }
                await _courseRepository.EditCourse(course);
                await _courseRepository.SaveChangesAsync();
                return EditCourseResult.success;
            }
            catch (Exception)
            {
                return EditCourseResult.error;
                throw;
            }
        }
        public async Task<EditCourseViewModel> GetInformationCourseForEdit(long Id)
        {
           return await _courseRepository.GetInformationCourseForEdit(Id);
        }
        public async Task<List<ShowCourseListItemViewModel>> GetCourses()
        {
            return await _courseRepository.GetCourses();
        }
        #endregion

        #region Filter Course
        public async Task<FilterCourseViewModel> FilterCourses(FilterCourseViewModel filterCourse)
        {
          return  await _courseRepository.FilterCoursesAsync(filterCourse);
        }

        public async Task<Course> GetCourseById(long Id)
        {
           return await _courseRepository.GetCourseByIdAsync(Id);
        }
        #endregion

        #region  Course Episode
        public async Task<CreateEpisodeResult> CreateCourseEpisode(CreateCourseEpisodeViewModel createCourse)
        {
            try
            {
                var newCourseEpisode = new CourseEpisode()
                {
                    CourseId = createCourse.CourseId,
                    EpisodeTime = createCourse.EpisodeTime,
                    EpisodeTitle = createCourse.EpisodeTitle,
                    IsFree = createCourse.IsFree,
                    EpisodeFileName = createCourse.EpisodeFileName.FileName
                };
                //check file name is Exist 
                if(await _courseRepository.CheckEpisodeFileNameIsExist(newCourseEpisode.EpisodeFileName))
                return CreateEpisodeResult.FileNameExist;

                //add file to server
                createCourse.EpisodeFileName.AddFileToServer(newCourseEpisode.EpisodeFileName, FilePaths.CourseEpisodeUploadPath);

                //add Course Episode 
                await _courseRepository.AddCourseEpisodeAsync(newCourseEpisode);
                await _courseRepository.SaveChangesAsync();
                return CreateEpisodeResult.Success;
            }
            catch (Exception)
            {
                return CreateEpisodeResult.Error;
                throw;
            }
        }
        public async Task<List<CourseEpisodeDetailViewModel>> GetEpisodesByCourseId(long courseId)
        {
            return await _courseRepository.GetEpisodesByCourseIdAsync(courseId);
        }
        public async Task<EditCourseEpisodeViewModel> GetEpisodesByIdForEdit(long episodeId)
        {
            return await _courseRepository.GetEpisodesByIdForEditAsync(episodeId);
                
        }
        public async Task<EditCourseEpisodeResult> EditCourseEpisode(EditCourseEpisodeViewModel editCourseEpisode)
        {
            try
            {
                var episode = await _courseRepository.GetEpisodeByIdAsync(editCourseEpisode.Id);
                if (episode == null)
                    return EditCourseEpisodeResult.Error;

                //update Episode
                episode.EpisodeTitle = editCourseEpisode.EpisodeTitle;
                episode.EpisodeTime = editCourseEpisode.EpisodeTime;
                episode.IsFree = editCourseEpisode.IsFree;

                //File

                if(editCourseEpisode.newEpisodeFileName != null)
                {
                    //check file name is Exist 
                    if (await _courseRepository.CheckEpisodeFileNameIsExist(editCourseEpisode.newEpisodeFileName.FileName))
                        return EditCourseEpisodeResult.FileNameExist;
        
                    //delete old file
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), FilePaths.CourseEpisodeUploadPath, episode.EpisodeFileName);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    //add new file to server
                    episode.EpisodeFileName = editCourseEpisode.newEpisodeFileName.FileName;
                    editCourseEpisode.newEpisodeFileName.AddFileToServer(episode.EpisodeFileName, FilePaths.CourseEpisodeUploadPath);

                }
                await _courseRepository.EditCourseEpisode(episode);
                return EditCourseEpisodeResult.Success;
                
            }
            catch (Exception)
            {
                return EditCourseEpisodeResult.Error;
                throw;
            }
        }
        #endregion

        #region Save Changes
        public async Task SaveChanges()
        {
            await _courseRepository.SaveChangesAsync();
        }
        #endregion
    }
}
