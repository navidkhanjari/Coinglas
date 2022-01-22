using Academy.Data.Context;
using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Permissions;
using Academy.Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Data.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        #region constructor
        private readonly ApplicationDbContext _context;
        public PermissionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Roles
        public async Task<List<Role>> GetRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task DeleteRolesAsync(long userId)
        {
            await _context.UserRoles
                .Where(u => u.UserId == userId)
                .ForEachAsync(r => _context.UserRoles.Remove(r));
        }
        public async Task AddUserRolesAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
        }

        public async Task AddRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }
        public async Task<Role> GetRoleByIdAsync(long roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }

        public async Task UpdateRole(Role role)
        {
            _context.Roles.Update(role);

            await SaveChangesAsync();
        }

        public async Task<List<long>> GetUserRolesAsync(long userId)
        {
            return await _context.UserRoles
                .Where(r => r.UserId == userId)
                .Select(r => r.RoleId)
                .ToListAsync();
        }

        #endregion

        #region Permission
        public async Task<List<Permission>> GetAllPermission()
        {
            return await _context.Permissions.ToListAsync();
        }
        public async Task AddPermissionToRoleAsync(long roleId, List<long> permission)
        {
            foreach (var p in permission)
            {
                await _context.AddAsync(new RolePermission()
                {
                    PermissionId = p,
                    RoleId = roleId
                });
            }
        }
        public async Task DeletePermissionRoleAsync(long roleId)
        {
            await _context.RolePermissions
                .Where(p => p.RoleId == roleId)
                .ForEachAsync(p => _context.RolePermissions.Remove(p));
        }
        public async Task<List<long>> GetAccessRolesAsync(long permissionId)
        {
            return await _context.RolePermissions
                .Where(p => p.PermissionId == permissionId)
                .Select(p => p.RoleId)
                .ToListAsync();
        }
        public async Task<List<long>> GetPermissionsRoleAsync(long roleId)
        {
            return await _context.RolePermissions
                  .Where(r => r.RoleId == roleId)
                  .Select(r => r.PermissionId)
                  .ToListAsync();
        }
        #endregion

        #region SaveChanges
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

      
        #endregion
    }
}
