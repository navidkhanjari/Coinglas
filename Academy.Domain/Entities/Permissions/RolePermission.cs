using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.Entities.Permissions
{
    public class RolePermission:BaseEntity
    {
        #region Properties
        public long RoleId { get; set; }
        public long PermissionId { get; set; }

        #endregion

        #region Relations
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        [ForeignKey("PermissionId")]
        public Permission Permission { get; set; }

        #endregion

    }
}
