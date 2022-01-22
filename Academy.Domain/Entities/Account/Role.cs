using Academy.Domain.Entities.Common;
using Academy.Domain.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.Entities.Account
{
    public class Role : BaseEntity
    {
        #region Properties
        [Display(Name = "نقش کاربر")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string RoleTitle { get; set; }
        public bool IsDelete { get; set; }
        #endregion

        #region Relations
        public ICollection<UserRole> UserRoles { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }

        #endregion

    }
}
