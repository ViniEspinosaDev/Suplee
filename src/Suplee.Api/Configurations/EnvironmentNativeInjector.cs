using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Core.API.Enviroment;
using Suplee.Core.Messages.Mail;

namespace Suplee.Api.Configurations
{
    public static class EnvironmentNativeInjector
    {
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
