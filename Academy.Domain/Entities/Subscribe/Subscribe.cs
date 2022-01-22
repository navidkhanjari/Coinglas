using Academy.Domain.Entities.Common;
using Academy.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.Entities.Subscribe
{
    public class Subscribe : BaseEntity
    {
        #region properties
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }
        [Display(Name = "قیمت(به تومان)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int Price { get; set; }
        [Display(Name = "ویژگی ها")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string Description { get; set; }
        #endregion

        #region relations
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public ICollection<UserSubscribes> UserSubscribes { get; set; }
        #endregion
    }
}
