using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.Resources.Settings
{
    public class SendGridEmailVariables
    {
        [JsonProperty("authWebUrl")]
        public string AuthWebUrl { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("name")]
        public string ToName { get; set; }
        [JsonProperty("email")]
        public string ToAddress { get; set; }
        [JsonProperty("company-name")]
        public string CompanyName { get; set; }
        [JsonProperty("company-email")]
        public string CompanyEmail { get; set; }
        public string TemplateId { get; set; }
    }
}
