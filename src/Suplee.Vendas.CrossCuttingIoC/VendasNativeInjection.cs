using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Vendas.Data;
using Suplee.Vendas.Data.Repository;
using Suplee.Vendas.Domain.Commands;
using Suplee.Vendas.Domain.Interfaces;

namespace Suplee.Vendas.CrossCuttingIoC
{
    public static class VendasNativeInjection
    {
        const string ConexaoSQL = "SqlConnection";

        public static void ConfigurarDependencias(IServiceCollection services, IConfiguration configuration)
        {
            ConfigurarDependenciasBancoDados(services, configuration);
            ConfigurarDependenciasCommand(services);
            ConfigurarDependenciasRepository(services);
            //ConfigurarDependenciasEvent(services);
        }

        private static void ConfigurarDependenciasRepository(IServiceCollection services)
        {
            services.AddScoped<IPedidoRepository, PedidoRepository>();
        }

        private static void ConfigurarDependenciasCommand(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<InserirProdutoCarrinhoCommand, bool>, PedidoCommandHandler>();
        }

        private static void ConfigurarDependenciasBancoDados(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<VendasContext>(options => options.UseSqlServer(configuration.GetConnectionString(ConexaoSQL)));
            //services.AddDbContext<AutenticacaoDbContext>(options => options.UseInMemoryDatabase("database"));
        }
    }
}
