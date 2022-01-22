
using Academy.Domain.Entities.Account;
using Academy.Domain.ViewModels.Paging;
using System;

namespace Academy.Domain.ViewModels.Account
{
   public class FilterUserViewModel:BasePaging<User>
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime? StartRegisterDate { get; set; }
        public DateTime? EndRegisterDate { get; set; }
    }
}
