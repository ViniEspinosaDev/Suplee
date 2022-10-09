using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Core.API.Enviroment;
using Suplee.Core.Messages.CommonMessages.IntegrationEvents;
using Suplee.Vendas.Data;
using Suplee.Vendas.Data.Repository;
using Suplee.Vendas.Domain.Commands;
using Suplee.Vendas.Domain.Events;
using Suplee.Vendas.Domain.Interfaces;

namespace Suplee.Vendas.CrossCuttingIoC
{
    public static class VendasNativeInjection
    {
        private static IEnvironment _environment;

        public static void ConfigurarDependencias(IEnvironment environment, IServiceCollection services)
        {
            _environment = environment;

            ConfigurarDependenciasBancoDados(services);
            ConfigurarDependenciasCommand(services);
            ConfigurarDependenciasRepository(services);
            ConfigurarDependenciasEvent(services);
        }

        private static void ConfigurarDependenciasEvent(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<PedidoRascunhoIniciadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoAtualizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoProdutoAdicionadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoProdutoAtualizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoProdutoRemovidoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoEstoqueRejeitadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PagamentoRealizadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PagamentoRecusadoEvent>, PedidoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoFinalizadoEvent>, PedidoEventHandler>();
        }

        private static void ConfigurarDependenciasRepository(IServiceCollection services)
        {
            services.AddScoped<IPedidoRepository, PedidoRepository>();
        }

        private static void ConfigurarDependenciasCommand(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<InserirProdutoCarrinhoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<ExcluirProdutoCarrinhoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarProdutoCarrinhoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<IniciarPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<FinalizarPedidoCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<CancelarProcessamentoPedidoEstornarEstoqueCommand, bool>, PedidoCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarCarrinhoCommand, bool>, PedidoCommandHandler>();
        }

        private static void ConfigurarDependenciasBancoDados(IServiceCollection services)
        {
            services.AddDbContext<VendasContext>(options => options.UseSqlServer(_environment.ConexaoSQL));
        }
    }
}
