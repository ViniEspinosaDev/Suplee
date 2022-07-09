using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Api.Extensions;
using Suplee.Identidade.Domain.Interfaces;

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
        public static IServiceCollection ConfigurarDependencias(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUsuario, AspNetUser>();

            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
