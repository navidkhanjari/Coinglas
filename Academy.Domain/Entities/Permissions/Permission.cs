using Academy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.Entities.Permissions
{
    public class Permission : BaseEntity
    {
        #region Properties
        [Display(Name = "عنوان ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(300, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string PermissionTitle { get; set; }

        public long? ParentId { get; set; }
        #endregion

        #region Relations

        [ForeignKey("ParentId")]
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }


        #endregion
    }
}
