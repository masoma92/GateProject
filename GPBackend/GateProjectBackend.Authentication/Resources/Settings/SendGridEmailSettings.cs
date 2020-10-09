using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.Resources.Settings
{
    public class SendGridEmailSettings
    {
        public string SendGridApiKey { get; set; }
        public string ResetPasswordTemplateId { get; set; }
        public string ConfirmEmailTemplateId { get; set; }
    }
}
