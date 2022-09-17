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
        const string cnxao = "Server=tcp:supleedatabase.clbx4cq85ddx.us-east-1.rds.amazonaws.com,1433; Initial Catalog=supleedatabase; User=supleeadmin; Password=SupleeDb123; MultipleActiveResultSets=True; Application Name=Suplee";

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
            //services.AddDbContext<TesteContext>(opt => opt.UseSqlServer(config.GetConnectionString(ConexaoSQL)));
            services.AddDbContext<TesteContext>(opt => opt.UseSqlServer(cnxao));
        }

        private static void ConfigurarDependenciasRepository(IServiceCollection services)
        {
            services.AddScoped<ITesteRepository, TesteRepository>();
        }
    }
}
