using BLL.Infrastructure;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailSenderService : Microsoft.AspNetCore.Identity.UI.Services.IEmailSender
    {
        private readonly SendGridSenderOptions _sendGridOptions;
        private readonly SendGridClient _sendGridClient;


        public EmailSenderService(IOptions<SendGridSenderOptions> options)
        {
            _sendGridOptions = options.Value;
            _sendGridClient = new SendGridClient(_sendGridOptions.SendGridKey)
;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await SendMaillAsync(email, subject, htmlMessage);
        }

        private async Task SendMaillAsync(string email, string subject, string htmlMessage)
        {
            var sendGridMessage = new SendGridMessage()
            {
                From = new EmailAddress(_sendGridOptions.UserMail),
                Subject = subject,
                PlainTextContent = htmlMessage
            };

            sendGridMessage.AddTo(email);
            await _sendGridClient.SendEmailAsync(sendGridMessage);
        }
    }
}
