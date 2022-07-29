using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Core.Tools.Image;

namespace Suplee.Api.Configurations
{
    /// <summary>
    /// Injeta dependências do Core
    /// </summary>
    public static class CoreNativeInjector
    {
        /// <summary>
        /// Injeta dependências do Core
        /// </summary>
        public static IServiceCollection ConfigurarDependencias(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IImageHelper, ImageHelper>();

            return services;
        }
    }
}
