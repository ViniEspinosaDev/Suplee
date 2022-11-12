namespace Suplee.Core.Messages.Mail
{
    public class MailConfiguration
    {
        public string SMTP { get; set; }
        public int Port { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public bool SandBox { get; set; }
        public bool UseSsl { get; set; }

        public override string ToString()
        {
            return $"SMTP: {SMTP} | Port: {Port} | Address: {Address} | Password: {Password} | SandBox: {SandBox} | UseSsl: {UseSsl}";
        }
    }

    public class ImgbbConfiguration
    {
        public string Url { get; set; }
        public string Expiration { get; set; }
        public string Key { get; set; }

        public override string ToString()
        {
            return $"Url: {Url} | Expiration: {Expiration} | Key: {Key}";
        }
    }

    public class ConnectionStringsConfiguration
    {
        public string SqlConnection { get; set; }
        public string EventStoreConnection { get; set; }
    }

    public class MongoDbConfiguration
    {
        public string Host { get; set; }
        public string Port { get; set; }

        public string ConnectionString { get => $"mongodb://{Host}:{Port}"; }
    }
}
