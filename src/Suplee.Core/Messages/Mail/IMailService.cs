using System.Threading.Tasks;

namespace Suplee.Core.Messages.Mail
{
    public interface IMailService
    {
        void OverrideMailConfiguration(MailConfiguration mailConfiguration);
        Task SendMailAsync(Mail destiny);
    }
}
