using Newtonsoft.Json;
using System;


namespace Academy.Application.Utils.Recaptcha
{
   public class RecaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("challenge_ts")]
        public DateTimeOffset ChallengeTs { get; set; }

        [JsonProperty("hostname")]
        public string HostName { get; set; }
    }
}
