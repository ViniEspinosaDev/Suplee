using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Core.Messages.CommonMessages.IntegrationEvents;
using Suplee.Pagamentos.AntiCorruption;
using Suplee.Pagamentos.Domain.Events;
using Suplee.Pagamentos.Domain.Interfaces;
using Suplee.Pagamentos.Domain.Services;

namespace Suplee.Pagamentos.CrossCuttingIoC
{
    public static class PagamentosNativeInjection
    {
        public static void ConfigurarDependencias(IServiceCollection services, IConfiguration configuration)
        {
            ConfigurarDependenciasService(services);
            ConfigurarDependenciasEvent(services);
        }

        private static void ConfigurarDependenciasService(IServiceCollection services)
        {
            services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreditoFacade>();
            services.AddScoped<IPagamentoService, PagamentoService>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
        }

        private static void ConfigurarDependenciasEvent(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<PedidoEstoqueConfirmadoEvent>, PagamentoEventHandler>();
        }
    }
}
