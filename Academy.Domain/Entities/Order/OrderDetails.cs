using Academy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.Entities.Order
{
    public class OrderDetails:BaseEntity
    {
        #region properties
        public long OrderId { get; set; }
        public long? SubscribeId { get; set; }
        public long? CourseId { get; set; }

        public int Price { get; set; }

        #endregion

        #region Relations
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("CourseId")]
        public Course.Course Course { get; set; }

        [ForeignKey("SubscribeId")]
        public Subscribe.Subscribe Subscribe { get; set; }
        #endregion
    }
}
