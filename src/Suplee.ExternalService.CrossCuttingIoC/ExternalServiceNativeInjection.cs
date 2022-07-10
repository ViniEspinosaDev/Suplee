using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.ExternalService.Imgbb.DTO;
using Suplee.ExternalService.Imgbb.Interfaces;
using Suplee.ExternalService.Imgbb.Services;

namespace Suplee.ExternalService.CrossCuttingIoC
{
    public static class ExternalServiceNativeInjection
    {
        public static void ConfigurarDependencias(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ImgbbConfiguracao>(configuration.GetSection("ExternalService:Imgbb"));

            services.AddSingleton<IImgbbService, ImgbbService>();
        }
    }
}
