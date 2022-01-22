using Academy.Application.Security;
using Academy.Application.Services.Interfaces;
using Academy.Domain.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Academy.Web.Areas.Admin.Controllers
{
    public class UsersController : AdminBaseController
    {
        #region constructor
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        public UsersController(IUserService userService, IPermissionService permissionService)
        {
            _userService = userService;
            _permissionService = permissionService;
        }
        #endregion

        #region filter users
        [PermissionChecker(2)]
        [HttpGet("/Admin/Users")]
        public async Task<IActionResult> Index(FilterUserViewModel filter)
        {
            return View(await _userService.FilterUsers(filter));
        }
        #endregion

        #region filter Deleted User
        [PermissionChecker(2)]
        [HttpGet("/Admin/DeletedUsers")]
        public async Task<IActionResult> DeletedUsers(FilterUserViewModel filter)
        {
            return View(await _userService.FilterDeletedUsers(filter));
        }
        #endregion

        #region Add User
        [PermissionChecker(6)]
        [HttpGet("/Admin/Users/Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["Roles"] = await _permissionService.GetRoles();
            return View();
        }
        [PermissionChecker(6)]
        [HttpPost("/Admin/Users/Create"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel user)
        {
            ViewData["Roles"] = await _permissionService.GetRoles();
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            var res = await _userService.CreateUserFromAdmin(user);
            switch (res)
            {
                case CreateUserResult.Success:
                    return RedirectToAction("Index");
                case CreateUserResult.UserExist:
                    ModelState.AddModelError("Email", "اطلاعات وارد شده قبلا در سیستم ثبت شده است");
                    break;
                case CreateUserResult.Error:
                    ModelState.AddModelError("Email", "ثبت کاربر جدید با خطا مواجه شد لطفا دوباره امتحان کنید!");
                    break;
            }
            return View(user);

        }
        #endregion

        #region Edit User
        [PermissionChecker(7)]

        [HttpGet("Admin/Users/Edit/{Id}")]
        public async Task<IActionResult> Edit(long Id)
        {
            var user = await _userService.GetUserByUserIdForShow(Id);

            ViewData["Roles"] = await _permissionService.GetRoles();

            return View(user);
        }

        [PermissionChecker(7)]
        [HttpPost("Admin/Users/Edit/{Id}"),ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel editUser)
        {
            ViewData["Roles"] = await _permissionService.GetRoles();
            if (!ModelState.IsValid)
            {
                return View(editUser);
            }
            var res = await _userService.EditUserFromAdmin(editUser);
            switch (res)
            {
                case EditUserResult.Success:
                   return RedirectToAction("Index");                   
                case EditUserResult.Error:
                    return NotFound();
            }
            return View(editUser);
        }
        #endregion

        #region Delete User
        [PermissionChecker(8)]
        [HttpGet("/Admin/Users/Delete/{Id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            var user = await _userService.GetUserInformationById(Id);
            if (user == null)
                return NotFound();
            ViewData["UserId"] = Id;
            return View(user);
        }
        [PermissionChecker(8)]
        [HttpPost("/Admin/Users/Delete/{Id}")]
        public async Task<IActionResult> DeleteConfirmed(long Id)
        {
            await _userService.DeleteUserFromAdmin(Id);

            return RedirectToAction("Index");
        }
        #endregion

    }
}
