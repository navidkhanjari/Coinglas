using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.Domain.ViewModels.Orders
{
   public class ShowOrderUserPanelViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int OrderSum { get; set; }
        public bool IsFinally { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
