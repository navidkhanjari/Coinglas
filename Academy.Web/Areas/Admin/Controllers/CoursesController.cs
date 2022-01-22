using Academy.Application.Security;
using Academy.Application.Services.Interfaces;
using Academy.Domain.ViewModels.Courses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.Areas.Admin.Controllers
{
    public class CoursesController : AdminBaseController
    {
        #region constructor
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }
        #endregion

        #region filterCourses

        [PermissionChecker(10002)]
        [HttpGet("Admin/Courses/Index")]
        public async Task<IActionResult> Index(FilterCourseViewModel filterCourse)
        {  
            return View(await _courseService.FilterCourses(filterCourse));
        }

        #endregion

        #region Create Course
        [PermissionChecker(10003)]
        [HttpGet("Admin/Courses/Create")]
        public async Task<IActionResult> Create()
        {
            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGroups = await _courseService.GetSubGroupForManageCourse(long.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text");

            var teachers = await _courseService.GetTeachers();
            ViewData["TeacherId"] = new SelectList(teachers, "Value", "Text");

            var levels = await _courseService.GetLevels();
            ViewData["LevelId"] = new SelectList(levels, "Value", "Text");

            var statuses = await _courseService.GetStatuses();
            ViewData["StatusId"] = new SelectList(statuses, "Value", "Text");
            return View();
        }

        [HttpPost("Admin/Courses/Create"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseViewModel createCourse)
        {
            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGroups = await _courseService.GetSubGroupForManageCourse(long.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text");

            var teachers = await _courseService.GetTeachers();
            ViewData["TeacherId"] = new SelectList(teachers, "Value", "Text");

            var levels = await _courseService.GetLevels();
            ViewData["LevelId"] = new SelectList(levels, "Value", "Text");

            var statuses = await _courseService.GetStatuses();
            ViewData["StatusId"] = new SelectList(statuses, "Value", "Text");

            if (!ModelState.IsValid)
                return View(createCourse);

           var res =  await _courseService.AddCourse(createCourse);
            switch (res)
            {
                case CreateCourseResult.Error:
                    ModelState.AddModelError("Title", "مشکلی رخ داد لطفا دوباره امتحان کنید");
                    break;
                case CreateCourseResult.Success:
                    return RedirectToAction("Index");
            }

            return View(createCourse);
        }
        #endregion

        #region Return Json SubGroups
        [HttpGet("Admin/GetSubGroupId/{Id}")]
        public async Task<IActionResult> GetSubGroupId(long Id)
        {
            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "انتخاب کنید",Value = ""}
            };
            list.AddRange( await _courseService.GetSubGroupForManageCourse(Id));
            return  Json(new SelectList(list, "Value", "Text"));
        }
        #endregion

        #region Edit Course
        [PermissionChecker(10004)]
        [HttpGet("Admin/Course/Edit/{Id}")]
        public async Task<IActionResult> Edit(long Id)
        {
            var course = await _courseService.GetInformationCourseForEdit(Id);
            if (course == null)
                return NotFound();


            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text",course.GroupId);

            var subGroups = await _courseService.GetSubGroupForManageCourse(long.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text",course.SubGroup??0);

            var teachers = await _courseService.GetTeachers();
            ViewData["TeacherId"] = new SelectList(teachers, "Value", "Text",course.TeacherId);

            var levels = await _courseService.GetLevels();
            ViewData["LevelId"] = new SelectList(levels, "Value", "Text",course.LevelId);

            var statuses = await _courseService.GetStatuses();
            ViewData["StatusId"] = new SelectList(statuses, "Value", "Text",course.StatusId);

            return View(course);
        }

        [HttpPost("Admin/Course/Edit/{Id}"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCourseViewModel editCourse)
        {
            if (!ModelState.IsValid)
                return View(editCourse);

            var course = await _courseService.GetCourseById(editCourse.Id);

            var groups = await _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text", course.GroupId);

            var subGroups = await _courseService.GetSubGroupForManageCourse(long.Parse(groups.First().Value));
            ViewData["SubGroups"] = new SelectList(subGroups, "Value", "Text", course.SubGroup ?? 0);

            var teachers = await _courseService.GetTeachers();
            ViewData["TeacherId"] = new SelectList(teachers, "Value", "Text", course.TeacherId);

            var levels = await _courseService.GetLevels();
            ViewData["LevelId"] = new SelectList(levels, "Value", "Text", course.LevelId);

            var statuses = await _courseService.GetStatuses();
            ViewData["StatusId"] = new SelectList(statuses, "Value", "Text", course.StatusId);

            //update
            var res = await _courseService.EditCourse(editCourse);
            switch (res)
            {
                case EditCourseResult.success:
                    return RedirectToAction("Index");
                case EditCourseResult.error:
                    ModelState.AddModelError("Title", "خطایی رخ داد , مجدد امتحان کنید");
                    break;
            }

            return View(editCourse);
        }
        #endregion

        #region Delete Course

        #endregion

        #region Details Course

        #endregion

        #region Create Episode
        [PermissionChecker(10003)]
        [HttpGet("Admin/Courses/CreateEpisode/{Id}")]
        public IActionResult CreateEpisode(long Id)
        {
            ViewData["CourseId"] = Id;
            return View("CreateEpisode");
        }

        [HttpPost("Admin/Courses/CreateEpisode/{Id}"),ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEpisode(CreateCourseEpisodeViewModel createEpisode,long Id)
        {
            ViewData["CourseId"] = Id;

            if (!ModelState.IsValid)
                return View(createEpisode);

            var res = await _courseService.CreateCourseEpisode(createEpisode);
            switch (res)
            {
                case CreateEpisodeResult.Success:
                    return RedirectToAction("Index");
                case CreateEpisodeResult.FileNameExist:
                    ModelState.AddModelError("EpisodeFileName", "نام فایل وارد شده تکراری است , لطفا تغییر دهید");
                    break;
                case CreateEpisodeResult.Error:
                    ModelState.AddModelError("EpisodeTitle", "خطایی رخ داد , مجدد امتحان کنید");
                    break;
            }

            return View(createEpisode);
        }
        #endregion

        #region Episode List
        [PermissionChecker(10002)]
        [HttpGet("Admin/Courses/EpisodeList/{Id}")]
        public async Task<IActionResult> EpisodeList(long Id)
        {
            var episodes = await _courseService.GetEpisodesByCourseId(Id);
            if ( episodes == null)
                return NotFound();

            return View("EpisodeList", episodes);
        }
        #endregion

        #region Edit Episode
        [PermissionChecker(10004)]
        [HttpGet("Admin/Course/EditEpisode/{Id}")]
        public async Task<IActionResult> EditEpisode(long Id)
        {
            var episode = await _courseService.GetEpisodesByIdForEdit(Id);
            if (episode == null)
                return NotFound();

            return View("EditEpisode",episode);
        }
        [HttpPost("Admin/Course/EditEpisode/{Id}"),ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEpisode(EditCourseEpisodeViewModel editCourseEpisode,long Id)
        {
            var episode = await _courseService.GetEpisodesByIdForEdit(Id);
            if (episode == null)
                return NotFound();
            var res = await _courseService.EditCourseEpisode(editCourseEpisode);
            switch (res)
            {
                case EditCourseEpisodeResult.Success:
                    return RedirectToAction("Index");
                case EditCourseEpisodeResult.Error:
                    ModelState.AddModelError("EpisodeTitle", "خطایی رخ داد , مجدد امتحان کنید");
                    break;
                case EditCourseEpisodeResult.FileNameExist:
                    ModelState.AddModelError("newEpisodeFileName", "نام فایل وارد شده تکراری است , لطفا تغییر دهید");
                    break;
            }
            return View("EditEpisode", episode);
        }
        #endregion
    }
}
