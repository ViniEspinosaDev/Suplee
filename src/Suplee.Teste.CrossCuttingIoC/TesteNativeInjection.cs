using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Core.API.Enviroment;
using Suplee.Teste.Data;
using Suplee.Teste.Data.Repository;
using Suplee.Teste.Domain.Commands;
using Suplee.Teste.Domain.Interfaces;

namespace Suplee.Teste.CrossCuttingIoC
{
    public static class TesteNativeInjection
    {
        private static IEnvironment _environment;

        public static void ConfigurarDependencias(IEnvironment environment, IServiceCollection services, IConfiguration config)
        {
            _environment = environment;

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
            services.AddDbContext<TesteContext>(opt => opt.UseSqlServer(config.GetConnectionString(_environment.ConexaoSQL)));
        }

        private static void ConfigurarDependenciasRepository(IServiceCollection services)
        {
            services.AddScoped<ITesteRepository, TesteRepository>();
        }
    }
}
