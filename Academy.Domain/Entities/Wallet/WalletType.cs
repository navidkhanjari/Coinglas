

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Academy.Domain.Entities.Wallet
{
    public class WalletType
    {
        #region Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long TypeId { get; set; }

        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string TypeTitle { get; set; }

        #endregion

        #region Relations
        public ICollection<Wallet> Wallet { get; set; }

        #endregion

    }
}
