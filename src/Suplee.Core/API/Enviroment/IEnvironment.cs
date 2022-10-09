using Suplee.Core.Messages.Mail;

namespace Suplee.Core.API.Enviroment
{
    public interface IEnvironment
    {
        string ConexaoSQL { get; }
        string ConexaoMongoDb { get; }
        MailConfiguration ConfiguracaoEmail { get; }
        ImgbbConfiguration ConfiguracaoImgbb { get; }
    }
}
