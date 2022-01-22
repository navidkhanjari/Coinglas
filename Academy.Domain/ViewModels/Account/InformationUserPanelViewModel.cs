using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.ViewModels.Account
{
   public class InformationUserPanelViewModel
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime RegisterDate { get; set; }
        public int Wallet { get; set; }
        public string PhoneNumber { get; set; }

    }
}
