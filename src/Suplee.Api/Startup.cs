using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Api.Configurations;
using Suplee.Catalogo.Api.Configurations.AutoMapper;
using Suplee.Catalogo.CrossCuttingIoC;
using Suplee.ExternalService.CrossCuttingIoC;
using Suplee.Identidade.CrossCuttingIoC;
using Suplee.Pagamentos.CrossCuttingIoC;
using Suplee.Teste.CrossCuttingIoC;
using Suplee.Vendas.CrossCuttingIoC;

namespace Suplee.Catalogo.Api
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup's constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Appsettings.json
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// // This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration();

            services.AddSwaggerConfiguration();

            services.AddAutoMapper(typeof(DomainToViewModelProfile), typeof(InputModelToDomainProfile));

            services.AddMediatR(typeof(Startup));

            services.ConfigurarDependencias(Configuration);

            ConfigurarDependencias(services);
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            app.UseApiConfiguration(env);

            app.UseSwaggerConfiguration(provider);
        }

        private void ConfigurarDependencias(IServiceCollection services)
        {
            CatalogoNativeInjection.ConfigurarDependencias(services, Configuration);
            CoreNativeInjector.ConfigurarDependencias(services, Configuration);
            ExternalServiceNativeInjection.ConfigurarDependencias(services, Configuration);
            IdentidadeNativeInjection.ConfigurarDependencias(services, Configuration);
            PagamentosNativeInjection.ConfigurarDependencias(services, Configuration);
            TesteNativeInjection.ConfigurarDependencias(services, Configuration);
            VendasNativeInjection.ConfigurarDependencias(services, Configuration);
        }
    }
}
