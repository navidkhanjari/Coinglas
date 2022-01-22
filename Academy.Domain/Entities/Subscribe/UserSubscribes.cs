using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.Entities.Subscribe
{
    public class UserSubscribes:BaseEntity
    {
        #region properties
        public long UserId { get; set; }
        public long SubscribeId { get; set; }
        public DateTime PaymentDay { get; set; }
        #endregion

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("SubscribeId")]
        public Subscribe Subscribe { get; set; }
        #endregion
    }
}
