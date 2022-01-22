using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Application.Services.Interfaces
{
    public interface IPermissionService
    {
        #region Roles
        Task<List<Role>> GetRoles();
        Task<long> AddRole(Role role);
        Task<Role> GetRoleByIdAsync(long roleId);
        Task UpdateRole(Role role);
        Task DeleteRole(long roleId);
        #endregion

        #region Permisson
        Task<List<Permission>> GetAllPermission();
        Task AddPermissionToRole(long roleId, List<long> permission);
        Task<List<long>> GetPermissionsRole(long roleId);
        Task UpdatePermissionRole(long roleId,List<long> permission);
        Task<bool> CheckPermission(string userName,long permissionId);
        #endregion

        #region SaveChanges
        Task SaveChanges();

        #endregion
    }
}
