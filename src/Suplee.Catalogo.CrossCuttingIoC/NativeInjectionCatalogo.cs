using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Catalogo.Data;
using Suplee.Catalogo.Data.Repository;
using Suplee.Catalogo.Domain.Interfaces;

namespace Suplee.Catalogo.CrossCuttingIoC
{
    public static class NativeInjectionCatalogo
    {
        public static void ConfigurarDependencias(IServiceCollection services, IConfiguration config)
        {
            // services.AddDbContext<CatalogoContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddDbContext<CatalogoContext>(opt => opt.UseSqlServer(config.GetConnectionString("SqlConnection")));

            ConfigurarDependenciasRepository(services);
        }

        private static void ConfigurarDependenciasRepository(IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
