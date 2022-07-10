using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Suplee.Api.Extensions;
using Suplee.ExternalService.Imgbb.DTO;
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
            services.Configure<ImgbbConfiguracao>(configuration.GetSection("ExternalService:Imgbb"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUsuario, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}
