using Academy.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Threading.Tasks;

namespace Academy.Web.Controllers
{
    public class HomeController : SiteBaseController
    {
        #region constructor
        private readonly IUserService _userService;
        private readonly ICourseService _courseService;
        public HomeController(IUserService userService,ICourseService courseService)
        {
            _courseService = courseService;
            _userService = userService;
        }
        #endregion
     
        #region Index
        public async Task<IActionResult> Index()
        {
            return View(await _courseService.GetCourses());
        }
        #endregion

        #region Online Payment

        string merchant = "****-****-****-****";

        [HttpGet("OnlinePayment/{id}")]
        public async Task<IActionResult> OnlinePayment(long id)
        {
            try
            {

                if (HttpContext.Request.Query["Authority"] != "")
                {
                    string authority = HttpContext.Request.Query["Authority"];
                }
                var wallet = await _userService.GetWalletByWalletId(id);

                string authorityverify = HttpContext.Request.Query["Authority"];

                string url = "https://api.zarinpal.com/pg/v4/payment/verify.json?merchant_id=" +
                    merchant + "&amount="
                    + wallet.Amount + "&authority="
                    + authorityverify;

                var client = new RestClient(url);
                client.Timeout = -1;

                var request = new RestRequest(Method.POST);

                request.AddHeader("accept", "application/json");

                request.AddHeader("content-type", "application/json");

                IRestResponse response = client.Execute(request);

                Newtonsoft.Json.Linq.JObject jodata = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
                string data = jodata["data"].ToString();

                Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(response.Content);
                string errors = jo["errors"].ToString();

                if (data != "[]")
                {
                    string refid = jodata["data"]["ref_id"].ToString();
                    ViewBag.code = refid;
                    ViewBag.IsSuccess = true;
                    wallet.IsPay = true;
                    await _userService.UpdateWallet(wallet);
                    return View();
                }
                else if (errors != "[]")
                {

                    string errorscode = jo["errors"]["code"].ToString();
                    ViewBag.IsSuccess = false;
                    return View();

                }


            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }


            return View();
        }
        #endregion

        #region Status Code
        [HttpGet("404")]
        public IActionResult NotFoundPage()
        {
            return View();
        }
        #endregion

        #region aboutUs

        [HttpGet("About-us")]
        public IActionResult AboutUs()
        {
            return View();
        }
        #endregion
    }
}
