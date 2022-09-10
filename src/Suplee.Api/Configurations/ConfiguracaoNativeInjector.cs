using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Suplee.Api.Extensions;
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
        /// <summary>
        /// Injeta as dependencias das configurações
        /// </summary>
        public static IServiceCollection ConfigurarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<MailConfiguration>(configuration.GetSection("MailConfiguration")).AddSingleton<IMailService, MailService>();

            services.AddScoped<IUsuarioLogado, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
