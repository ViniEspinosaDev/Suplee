using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Suplee.Core.Messages.Mail
{
    public class MailService : IMailService
    {
        private MailConfiguration _mailConfiguration;

        public MailService(IOptions<MailConfiguration> mailConfigurationDefault)
        {
            _mailConfiguration = mailConfigurationDefault.Value;
        }

        public void OverrideMailConfiguration(MailConfiguration mailConfiguration)
        {
            _mailConfiguration = mailConfiguration;
        }

        public async Task SendMailAsync(Mail destiny)
        {
            using SmtpClient smtpClient = new SmtpClient(_mailConfiguration.SMTP, _mailConfiguration.Port)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_mailConfiguration.Address, _mailConfiguration.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = _mailConfiguration.UseSsl,
            };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var destino = destiny.MailAddress;

            MailMessage mail = new MailMessage(_mailConfiguration.Address, destino)
            {
                Subject = destiny.Subject,
                IsBodyHtml = true,
                Body = destiny.BodyText
            };

            if (destiny.Attachments != null)
            {
                foreach (var attachment in destiny.Attachments)
                    mail.Attachments.Add(attachment);
            }

            await smtpClient.SendMailAsync(mail);
        }
    }
}
