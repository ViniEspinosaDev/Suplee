using Microsoft.Extensions.DependencyInjection;
using Suplee.Core.API.Enviroment;
using Suplee.ExternalService.Imgbb.Interfaces;
using Suplee.ExternalService.Imgbb.Services;

namespace Suplee.ExternalService.CrossCuttingIoC
{
    public static class ExternalServiceNativeInjection
    {
        private static IEnvironment _environment;

        public static void ConfigurarDependencias(IEnvironment environment, IServiceCollection services)
        {
            _environment = environment;

            services.AddSingleton<IImgbbService>(s => new ImgbbService(_environment));
        }
    }
}
