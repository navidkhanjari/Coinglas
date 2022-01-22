
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Academy.Domain.ViewModels.Wallet
{
    public class ChargeWalletViewModel
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int Amount { get; set; }

    }
}
