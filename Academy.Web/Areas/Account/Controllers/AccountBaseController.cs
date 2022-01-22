using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Academy.Web.Areas.Account.Controllers
{
    [Area("Account")]
    [Authorize]
    public class AccountBaseController : Controller { }
}
