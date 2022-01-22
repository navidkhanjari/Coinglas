using Academy.Application.Services.Interfaces;
using Academy.Domain.ViewModels.Wallet;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Academy.Web.Areas.Account.Controllers
{
    public class WalletController : AccountBaseController
    {
        #region constructor
        private readonly IUserService _userService;
        public WalletController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Index
        [HttpGet("Account/Wallet")]
        public async Task<IActionResult> Index(bool finaly = false)
        {
            ViewBag.IsFinaly = finaly;
            return View(await _userService.GetUserWallet(User.Identity.Name));
        }
        #endregion

        #region Charge Wallet 
        string merchant = "****-****-****-****-****";
        string authority;
        string callbackurl = "https://iaminmoradi.ir/OnlinePayment/";

        [HttpGet("Account/ChargeWallet")]
        public IActionResult ChargeWallet()
        {
            return View();
        }
        [HttpPost("Account/ChargeWallet"), ValidateAntiForgeryToken]
        public async Task<IActionResult> ChargeWallet(ChargeWalletViewModel charge)
        {
            if (!ModelState.IsValid)
            {
                return View(charge);
            }
            long walletId = await _userService.ChargeWallet(User.Identity.Name, "شارژ حساب", charge.Amount);

            //Online Payment


            #region Online Payment
            try
            {


                //be dalil in ke metadata be sorate araye ast va do meghdare mobile va email dar metadata gharar mmigirad
                //shoma mitavanid in maghadir ra az kharidar begirid va set konid dar gheir in sorat khali ersal konid

                string requesturl;
                requesturl = "https://api.zarinpal.com/pg/v4/payment/request.json?merchant_id=" +
                    merchant + "&amount=" + charge.Amount +
                    "&callback_url=" + callbackurl + walletId +
                    "&description=" + "شارژ حساب";

                


                var client = new RestClient(requesturl);

                client.Timeout = -1;

                var request = new RestRequest(Method.POST);

                request.AddHeader("accept", "application/json");

                request.AddHeader("content-type", "application/json");

                IRestResponse requestresponse = client.Execute(request);

                Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Content);
                string errorscode = jo["errors"].ToString();

                Newtonsoft.Json.Linq.JObject jodata = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Content);
                string dataauth = jodata["data"].ToString();


                if (dataauth != "[]")
                {


                    authority = jodata["data"]["authority"].ToString();
                    string gatewayUrl = "https://www.zarinpal.com/pg/StartPay/" + authority;
                    return Redirect(gatewayUrl);

                }
                else
                {

                    //return BadRequest();
                    return BadRequest("error " + errorscode);
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            #endregion


        }
        #endregion

     
    }
}
