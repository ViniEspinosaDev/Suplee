using Microsoft.Extensions.Options;
using Suplee.Core.Messages.Mail;
using System;

namespace Suplee.Core.API.Enviroment
{
    public class EnvironmentVariable : IEnvironment
    {
        private readonly MailConfiguration _mailConfiguration;
        private readonly ConnectionStringsConfiguration _connectionStringsConfiguration;
        private readonly ImgbbConfiguration _imgbbConfiguration;

        public EnvironmentVariable(
            IOptions<MailConfiguration> mailConfiguration,
            IOptions<ConnectionStringsConfiguration> connectionStringConfiguration,
            IOptions<ImgbbConfiguration> imgbbConfiguration)
        {
            _mailConfiguration = mailConfiguration.Value;
            _connectionStringsConfiguration = connectionStringConfiguration.Value;
            _imgbbConfiguration = imgbbConfiguration.Value;
        }

        private string SQL_Servidor => RecuperarVariavelAmbiente(EnvironmentConstant.SQL_Servidor);

        private string SQL_NomeBD => RecuperarVariavelAmbiente(EnvironmentConstant.SQL_NomeBD);

        private string SQL_Usuario => RecuperarVariavelAmbiente(EnvironmentConstant.SQL_Usuario);

        private string SQL_Senha => RecuperarVariavelAmbiente(EnvironmentConstant.SQL_Senha);

        private string SQL_NomeAPP => RecuperarVariavelAmbiente(EnvironmentConstant.SQL_NomeAPP);

        private string IMGBB_URL => RecuperarVariavelAmbiente(EnvironmentConstant.IMGBB_URL);

        private string IMGBB_TempoExpiracao => RecuperarVariavelAmbiente(EnvironmentConstant.IMGBB_TempoExpiracao);

        private string IMGBB_Chave => RecuperarVariavelAmbiente(EnvironmentConstant.IMGBB_Chave);

        private string MAIL_SMTP => RecuperarVariavelAmbiente(EnvironmentConstant.MAIL_SMTP);

        private string MAIL_Porta => RecuperarVariavelAmbiente(EnvironmentConstant.MAIL_Porta);

        private string MAIL_Endereco => RecuperarVariavelAmbiente(EnvironmentConstant.MAIL_Endereco);

        private string MAIL_Senha => RecuperarVariavelAmbiente(EnvironmentConstant.MAIL_Senha);

        private string MAIL_UsarSSL => RecuperarVariavelAmbiente(EnvironmentConstant.MAIL_UsarSSL);

        private bool Desenvolvimento => Environment.GetEnvironmentVariable(EnvironmentConstant.Ambiente) == "Development";

        private string RecuperarVariavelAmbiente(string rotulo) => Environment.GetEnvironmentVariable(rotulo);

        public string ConexaoSQL
        {
            get
            {
                if (Desenvolvimento)
                    return _connectionStringsConfiguration.SqlConnection;

                return $"Server={SQL_Servidor}; Initial Catalog={SQL_NomeBD}; User={SQL_Usuario}; Password={SQL_Senha}; MultipleActiveResultSets=True; Application Name={SQL_NomeAPP}";
            }
        }

        public string ConexaoMongoDb
        {
            get
            {
                if (Desenvolvimento)
                    return _connectionStringsConfiguration.MongoConnection;

                return $"Necessário configurar conexão Mongo para ambiente - {Environment.GetEnvironmentVariable(EnvironmentConstant.Ambiente)}";
            }
        }

        public MailConfiguration ConfiguracaoEmail
        {
            get
            {
                if (Desenvolvimento)
                    return _mailConfiguration;

                return new MailConfiguration()
                {
                    SMTP = MAIL_SMTP,
                    Port = int.Parse(MAIL_Porta),
                    Address = MAIL_Endereco,
                    Password = MAIL_Senha,
                    SandBox = true,
                    UseSsl = bool.Parse(MAIL_UsarSSL)
                };
            }
        }

        public ImgbbConfiguration ConfiguracaoImgbb
        {
            get
            {
                if (Desenvolvimento)
                    return _imgbbConfiguration;

                return new ImgbbConfiguration()
                {
                    Url = IMGBB_URL,
                    Expiration = IMGBB_TempoExpiracao,
                    Key = IMGBB_Chave
                };
            }
        }
    }
}
