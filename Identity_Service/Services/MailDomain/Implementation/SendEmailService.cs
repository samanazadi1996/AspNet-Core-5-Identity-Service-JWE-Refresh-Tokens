using Services.MailDomain.Abstraction;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Services.MailDomain.Implementation
{
    public class SendEmailService : ISendEmailService
    {
        public readonly ILogger<SendEmailService> logger;
        private readonly SiteSettings _SiteSetting;

        public SendEmailService(ILogger<SendEmailService> logger, IOptionsSnapshot<SiteSettings> Setting)
        {
            _SiteSetting = Setting.Value;
            this.logger = logger;
        }
        public async Task SendAsync(string to, string body)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("info@bycheck.ir");
                message.To.Add(new MailAddress(to));
                message.Subject = "بای چک";
                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;
                message.Body = body;
                smtp.Port = _SiteSetting.MailSettings.SmtpPort;
                smtp.Host = _SiteSetting.MailSettings.SmtpHost;
                smtp.EnableSsl = false;
                smtp.Timeout = 20000;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_SiteSetting.MailSettings.SmtpUser, _SiteSetting.MailSettings.SmtpPass);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                await smtp.SendMailAsync(message);
                logger.LogInformation("[Email] Email successfuly sent to {EmailAddress}.", to);
            }
            catch (Exception exp)
            {
                logger.LogError(exp, "[Email] An exception was throw while sending email to {EmailAddress}.", to);
            }
        }
    }
}
