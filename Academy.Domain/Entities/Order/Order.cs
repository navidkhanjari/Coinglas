using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Academy.Domain.Entities.Order
{
    public class Order:BaseEntity
    {
        #region properties
        public long UserId { get; set; }
        public int OrderSum { get; set; }
        public bool IsFinally { get; set; }
        public DateTime CreateDate { get; set; }

        #endregion

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        #endregion
    }
}
