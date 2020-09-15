using GateProjectBackend.Authentication.Resources.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.Helpers
{
    public interface IEmailSender
    {
        Task<object> SendAsync(SendGridEmailVariables variables);
    }

    public class EmailSender : IEmailSender
    {
        private readonly IOptions<SendGridEmailSettings> _sendGridEmailSettings;

        public EmailSender(IOptions<SendGridEmailSettings> sendGridEmailSettings)
        {
            _sendGridEmailSettings = sendGridEmailSettings;
        }

        public async Task<object> SendAsync(SendGridEmailVariables variables)
        {
            var sendGridClient = new SendGridClient(_sendGridEmailSettings.Value.SendGridApiKey);

            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom(variables.CompanyEmail, variables.CompanyName);
            sendGridMessage.AddTo(variables.ToAddress, variables.ToName);
            sendGridMessage.SetTemplateId(variables.TemplateId);
            sendGridMessage.SetTemplateData(variables);

            var response = await sendGridClient.SendEmailAsync(sendGridMessage);

            return response;
        }
    }
}
