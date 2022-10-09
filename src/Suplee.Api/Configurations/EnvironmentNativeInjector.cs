using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Core.API.Enviroment;
using Suplee.Core.Messages.Mail;

namespace Suplee.Api.Configurations
{
    /// <summary>
    /// Organizar as variáveis de ambiente
    /// </summary>
    public static class EnvironmentNativeInjector
    {
        /// <summary>
        /// 
        /// </summary>
        public static void ConfigurarVariaveisAmbiente(IServiceCollection services, IConfiguration configuration)
        {
            services
                .Configure<MailConfiguration>(configuration.GetSection("MailConfiguration"))
                .Configure<ConnectionStringsConfiguration>(configuration.GetSection("ConnectionStrings"))
                .Configure<ImgbbConfiguration>(configuration.GetSection("ExternalService:Imgbb"))
                .AddScoped<IEnvironment, EnvironmentVariable>();
        }
    }
}
