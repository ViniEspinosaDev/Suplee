using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Suplee.Core.Messages.Mail
{
    public class MailService : IMailService
    {
        private MailConfiguration _mailConfiguration;

        private string end = "suplee.app@gmail.com";
        private bool ssl = true;
        private string smtp = "smtp.gmail.com";
        private int prt = 587;
        private string snh = "gxfnpqxpcdhaiedm";


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
            using SmtpClient smtpClient = new SmtpClient(smtp, prt)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(end, snh),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = ssl,
            };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var destino = destiny.MailAddress;

            MailMessage mail = new MailMessage(end, destino)
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
