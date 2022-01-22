using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.ViewModels.Subscribes
{
   public  class ShowSubscribeBuyersViewModel
    {
        public long UserId { get; set; }
        public long SubscribeId { get; set; }
        public DateTime PaymentDay { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
}
