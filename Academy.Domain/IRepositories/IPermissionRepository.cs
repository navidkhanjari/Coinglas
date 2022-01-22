using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.IRepositories
{
   public interface IPermissionRepository
    {

        #region Roles
        Task<List<Role>> GetRolesAsync();
        Task AddUserRolesAsync(UserRole userRole);
        Task DeleteRolesAsync(long userId);
        Task AddRoleAsync(Role role);
        Task<Role> GetRoleByIdAsync(long roleId);
        Task UpdateRole(Role role);
        Task<List<long>> GetUserRolesAsync(long userId);
        
        #endregion

        #region Permissions
        Task<List<Permission>> GetAllPermission();
        Task AddPermissionToRoleAsync(long roleId,List<long> permission);
        Task<List<long>> GetPermissionsRoleAsync(long roleId);
        Task DeletePermissionRoleAsync(long roleId);
        Task<List<long>> GetAccessRolesAsync(long permissionId);

       
        #endregion

        #region SaveChanges
        Task SaveChangesAsync();
        #endregion
    }
}
