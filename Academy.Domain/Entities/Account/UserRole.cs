using Academy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.Entities.Account
{
   public class UserRole:BaseEntity
    {
        #region Properties
        public long UserId { get; set; }
        public long RoleId { get; set; }
        #endregion

        #region Relations
        public User User { get; set; }
        public Role Role { get; set; }
        #endregion

    }
}
