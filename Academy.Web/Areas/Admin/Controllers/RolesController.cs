using Academy.Application.Security;
using Academy.Application.Services.Interfaces;
using Academy.Domain.Entities.Account;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.Areas.Admin.Controllers
{

    public class RolesController : AdminBaseController
    {
        #region constructor
        private readonly IPermissionService _permissionService;
        public RolesController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }
        #endregion

        #region Index
        [PermissionChecker(4)]
        [HttpGet("Admin/Roles")]
        public async Task<IActionResult> Index()
        {
            var Roles = await _permissionService.GetRoles();

            return View(Roles);
        }
        #endregion

        #region Create Role
        [PermissionChecker(4)]

        [HttpGet("Admin/Roles/Create")]
        public async Task<IActionResult> Create()
        {
            ViewData["Permissions"] = await _permissionService.GetAllPermission();
            return View();
        }
        [HttpPost("Admin/Roles/Create")]
        public async Task<IActionResult> Create(Role role, List<long> SelectedPermission)
        {
            if (!ModelState.IsValid)
            {
                return View(role);
            }

            //Add Role
            long roleId = await _permissionService.AddRole(role);

            //Add Permissions Role
            await _permissionService.AddPermissionToRole(roleId, SelectedPermission);

            return RedirectToAction("Index");
        }
        #endregion

        #region Edit Role
        [PermissionChecker(4)]

        [HttpGet("Admin/Roles/Edit/{Id}")]
        public async Task<IActionResult> Edit(long Id)
        {
            ViewData["Permissions"] = await _permissionService.GetAllPermission();
            ViewData["SelectedPermission"] = await _permissionService.GetPermissionsRole(Id);
            var role = await _permissionService.GetRoleByIdAsync(Id);

            if (role == null)
                return NotFound();

            return View(role);
        }
        [HttpPost("Admin/Roles/Edit/{Id}")]
        public async Task<IActionResult> Edit(Role EditRole, List<long> SelectedPermission)
        {
            ViewData["Permissions"] = await _permissionService.GetAllPermission();
            ViewData["SelectedPermission"] = await _permissionService.GetPermissionsRole(EditRole.Id);
            if (!ModelState.IsValid)
                return View(EditRole);

            //Update Role
            await _permissionService.UpdateRole(EditRole);

            //Update Permissions Role
            await _permissionService.UpdatePermissionRole(EditRole.Id, SelectedPermission);

            await _permissionService.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion

        #region Delete Role
        [PermissionChecker(4)]

        [HttpGet("Admin/Roles/Delete/{Id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            ViewData["Permissions"] = await _permissionService.GetAllPermission();
            ViewData["SelectedPermission"] = await _permissionService.GetPermissionsRole(Id);

            var role = await _permissionService.GetRoleByIdAsync(Id);
            if (role == null)
                return NotFound();

            return View(role);
        }

        [HttpPost("Admin/Roles/Delete/{Id}")]
        public async Task<IActionResult> DeleteConfirmed(long Id)
        {
            ViewData["Permissions"] = await _permissionService.GetAllPermission();
            ViewData["SelectedPermission"] = await _permissionService.GetPermissionsRole(Id);

            await _permissionService.DeleteRole(Id);

            return RedirectToAction("Index");
        }
        #endregion

    }
}
