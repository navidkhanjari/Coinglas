using Academy.Domain.Entities.Common;
using Academy.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.Entities.Account
{
    public class UserDiscountCode:BaseEntity
    {
        #region properties
        public long UserId { get; set; }
        public long DiscountId { get; set; }
        #endregion

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("DiscountId")]
        public Discount Discount { get; set; }
        #endregion
    }
}
