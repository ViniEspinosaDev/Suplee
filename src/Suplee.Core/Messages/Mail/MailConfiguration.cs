namespace Suplee.Core.Messages.Mail
{
    public class MailConfiguration
    {
        public MailConfiguration() { }

        public MailConfiguration(string sMTP,
                                 int port,
                                 string address,
                                 string password,
                                 bool sandBox,
                                 bool ssl)
        {
            SMTP = sMTP;
            Port = port;
            Address = address;
            Password = password;
            SandBox = sandBox;
            UseSsl = ssl;
        }

        public string SMTP { get; set; }
        public int Port { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public bool SandBox { get; set; }
        public bool UseSsl { get; set; }
    }
}
