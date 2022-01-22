
using Academy.Domain.Entities.Account;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Domain.Entities.Wallet
{
    public class Wallet
    {
        #region Properties
        [Key]
        public long WalletId { get; set; }

        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        
        public long TypeId { get; set; }

        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public long UserId { get; set; }

        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }

        [Display(Name = "شرح")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Description { get; set; }

        [Display(Name = "تایید شده")]
        public bool IsPay { get; set; }

        [Display(Name = "تاریخ و ساعت")]
        public DateTime PaymentDate { get; set; }
        #endregion

        #region Relations

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("TypeId")]
        public WalletType WalletType { get; set; }
        #endregion
    }
}
