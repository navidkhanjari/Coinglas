using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.ViewModels.Wallet
{
    public class WalletViewModel
    {
        public int Amount { get; set; }
        public long Type { get; set; }
        public string Description { get; set; }
        public DateTime PaymentDate { get; set; }    

    }
}
