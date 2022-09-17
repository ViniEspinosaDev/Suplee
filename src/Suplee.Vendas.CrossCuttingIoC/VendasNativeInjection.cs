using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        const string ConexaoSQL = "SqlConnection";
        const string cnxao = "Server=tcp:supleedatabase.clbx4cq85ddx.us-east-1.rds.amazonaws.com,1433; Initial Catalog=supleedatabase; User=supleeadmin; Password=SupleeDb123; MultipleActiveResultSets=True; Application Name=Suplee";

        public static void ConfigurarDependencias(IServiceCollection services, IConfiguration configuration)
        {
            ConfigurarDependenciasBancoDados(services, configuration);
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

        private static void ConfigurarDependenciasBancoDados(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<VendasContext>(options => options.UseSqlServer(configuration.GetConnectionString(ConexaoSQL)));
            services.AddDbContext<VendasContext>(options => options.UseSqlServer(cnxao));
            //services.AddDbContext<AutenticacaoDbContext>(options => options.UseInMemoryDatabase("database"));
        }
    }
}
