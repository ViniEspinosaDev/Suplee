using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.ExternalService.Imgbb.Interfaces;
using Suplee.ExternalService.Imgbb.Services;

namespace Suplee.ExternalService.CrossCuttingIoC
{
    public static class NativeInjectionExternalService
    {
        public static void ConfigurarDependencias(IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IImgbbService, ImgbbService>();
        }
    }
}
