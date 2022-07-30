using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Teste.Data;
using Suplee.Teste.Data.Repository;
using Suplee.Teste.Domain.Commands;
using Suplee.Teste.Domain.Interfaces;

namespace Suplee.Teste.CrossCuttingIoC
{
    public static class TesteNativeInjection
    {
        const string ConexaoSQL = "SqlConnection";

        public static void ConfigurarDependencias(IServiceCollection services, IConfiguration config)
        {
            ConfigurarDependenciasDatabase(services, config);
            ConfigurarDependenciasRepository(services);
            ConfigurarDependenciasCommand(services);
        }

        private static void ConfigurarDependenciasCommand(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CadastrarImagemCommand, bool>, TesteCommandHandler>();
        }

        private static void ConfigurarDependenciasDatabase(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<TesteContext>(opt => opt.UseSqlServer(config.GetConnectionString(ConexaoSQL)));
        }

        private static void ConfigurarDependenciasRepository(IServiceCollection services)
        {
            services.AddScoped<ITesteRepository, TesteRepository>();
        }
    }
}
