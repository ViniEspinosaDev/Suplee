using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Suplee.Api.Extensions;
using Suplee.Core.API.Enviroment;
using Suplee.Core.Messages.Mail;
using Suplee.Identidade.Domain.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Suplee.Api.Configurations
{
    /// <summary>
    /// Injeta as dependencias das configurações
    /// </summary>
    public static class ConfiguracaoNativeInjector
    {
        private static IEnvironment _environment;

        /// <summary>
        /// Injeta as dependencias das configurações
        /// </summary>
        public static void ConfigurarDependencias(IEnvironment environment, IServiceCollection services, IConfiguration configuration)
        {
            _environment = environment;

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IMailService>(s => new MailService(_environment));

            services.AddScoped<IUsuarioLogado, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }
    }
}
