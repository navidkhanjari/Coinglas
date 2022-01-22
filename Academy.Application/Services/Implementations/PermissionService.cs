using Academy.Application.Services.Interfaces;
using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Permissions;
using Academy.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.Implementations
{
    public class PermissionService : IPermissionService
    {
        #region constructor
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUserService _userService;

        public PermissionService(IPermissionRepository permissionRepository, IUserService userService)
        {
            _permissionRepository = permissionRepository;
            _userService = userService;
        }
        #endregion

        #region Roles
        public async Task<List<Role>> GetRoles()
        {
            return await _permissionRepository.GetRolesAsync();
        }
        public async Task<long> AddRole(Role role)
        {
            await _permissionRepository.AddRoleAsync(role);
            await SaveChanges();
            return role.Id;
        }

        public async Task<Role> GetRoleByIdAsync(long roleId)
        {
            return await _permissionRepository.GetRoleByIdAsync(roleId);
        }
        public async Task UpdateRole(Role role)
        {
            await _permissionRepository.UpdateRole(role);
        }

        public async Task DeleteRole(long roleId)
        {
            var role = await _permissionRepository.GetRoleByIdAsync(roleId);
            role.IsDelete = true;
            await UpdateRole(role);
        }
        #endregion

        #region Permission
        public async Task<List<Permission>> GetAllPermission()
        {
           return await _permissionRepository.GetAllPermission();
        }
        public async Task AddPermissionToRole(long roleId, List<long> permission)
        {
            await _permissionRepository.AddPermissionToRoleAsync(roleId, permission);
            await SaveChanges();
        }

        public async Task<List<long>> GetPermissionsRole(long roleId)
        {
           return await _permissionRepository.GetPermissionsRoleAsync(roleId);
        }
        public async Task UpdatePermissionRole(long roleId, List<long> permission)
        {
            //Delete Permission
            await _permissionRepository.DeletePermissionRoleAsync(roleId);
            //Update Permission
            await _permissionRepository.AddPermissionToRoleAsync(roleId,permission);
        }
        public async Task<bool> CheckPermission(string userName, long permissionId)
        {
            long userId = await _userService.GetUserIdByUserName(userName);

            //Get User Roles
            List<long> UserRoles = await _permissionRepository.GetUserRolesAsync(userId);

            if (!UserRoles.Any())
                return false;
            //Get RolesPermission

            List<long> RolePermission = await _permissionRepository.GetAccessRolesAsync(permissionId);

            return RolePermission.Any(p => UserRoles.Contains(p));
            
        }
        #endregion

        #region SaveChanges
        public async Task SaveChanges()
        {
            await _permissionRepository.SaveChangesAsync();
        }

        #endregion
    }
}

