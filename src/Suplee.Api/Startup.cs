using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Api.Configurations;
using Suplee.Catalogo.Api.Configurations.AutoMapper;
using Suplee.Catalogo.CrossCuttingIoC;
using Suplee.Core.API.Enviroment;
using Suplee.ExternalService.CrossCuttingIoC;
using Suplee.Identidade.CrossCuttingIoC;
using Suplee.Pagamentos.CrossCuttingIoC;
using Suplee.Teste.CrossCuttingIoC;
using Suplee.Vendas.CrossCuttingIoC;

namespace Suplee.Catalogo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration();

            services.AddSwaggerConfiguration();

            services.AddAutoMapper(typeof(DomainToViewModelProfile), typeof(InputModelToDomainProfile));

            services.AddMediatR(typeof(Startup));

            ConfigurarDependencias(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseApiConfiguration(env);

            app.UseSwaggerConfiguration(provider);
        }

        private void ConfigurarDependencias(IServiceCollection services)
        {
            CoreNativeInjector.ConfigurarDependencias(services);
            EnvironmentNativeInjector.ConfigurarVariaveisAmbiente(services, Configuration);

            var serviceProvider = services.BuildServiceProvider();

            var environment = (IEnvironment)serviceProvider.GetService(typeof(IEnvironment));

            ConfiguracaoNativeInjector.ConfigurarDependencias(environment, services, Configuration);
            IdentidadeNativeInjection.ConfigurarDependencias(environment, services, Configuration);
            CatalogoNativeInjection.ConfigurarDependencias(environment, services, Configuration);
            ExternalServiceNativeInjection.ConfigurarDependencias(environment, services);
            PagamentosNativeInjection.ConfigurarDependencias(services, Configuration);
            TesteNativeInjection.ConfigurarDependencias(environment, services, Configuration);
            VendasNativeInjection.ConfigurarDependencias(environment, services);
        }
    }
}
