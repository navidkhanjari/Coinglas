using Academy.Domain.Entities.Account;
using Academy.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Academy.Domain.Entities.Order
{
    public class Discount : BaseEntity
    {

        #region properties
        [Required]
        [MaxLength(150)]
        public string DiscountCode { get; set; }

        [Required]
        public int DiscountPercent { get; set; }

        public int? UsableCount { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        #endregion

        #region relations
        public ICollection<UserDiscountCode> UserDiscountCodes { get; set; }
        #endregion

    }
}
