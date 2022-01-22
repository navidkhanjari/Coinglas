using Academy.Application.Services.Interfaces;
using Academy.Application.Utils.Recaptcha;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Academy.Application.Services.Implementations
{
    public class GoogleRecaptcha : IGoogleRecaptcha
    {
        #region constructor
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;
        public GoogleRecaptcha(IConfiguration configuration, IHttpContextAccessor accessor)
        {
            _configuration = configuration;
            _accessor = accessor;
        }
        #endregion
        public async Task<bool> IsConfirmed()
        {
            var secretKey = _configuration.GetSection("GoogleRecaptcha")["SecretKey"];
            var response = _accessor.HttpContext.Request.Form["g-recaptcha-response"];

            var http = new HttpClient();

            var result = await http.PostAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={response}", null);

            if (result.IsSuccessStatusCode)
            {

                var googleResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(await result.Content.ReadAsStringAsync());

                if (googleResponse == null)
                    return false;

                return googleResponse.Success;
            }

            return false;
        }
    }
}
