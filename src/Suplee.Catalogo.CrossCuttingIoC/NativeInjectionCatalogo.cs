using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Catalogo.Data;
using Suplee.Catalogo.Data.Repository;
using Suplee.Catalogo.Domain.Commands;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Data.EventSourcing;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.EventSourcing;

namespace Suplee.Catalogo.CrossCuttingIoC
{
    public static class NativeInjectionCatalogo
    {
        const string ConexaoSQL = "SqlConnection";

        public static void ConfigurarDependencias(IServiceCollection services, IConfiguration config)
        {
            ConfigurarDependenciasPadrao(services);

            ConfigurarDependenciasDatabase(services, config);
            ConfigurarDependenciasRepository(services);
            ConfigurarDependenciasCommand(services);
        }

        private static void ConfigurarDependenciasPadrao(IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Event Sourcing
            services.AddSingleton<IEventStoreService, EventStoreService>();
            services.AddSingleton<IEventSourcingRepository, EventSourcingRepository>();
        }

        private static void ConfigurarDependenciasCommand(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AdicionarProdutoCommand, bool>, CatalogoCommandHandler>();
        }

        private static void ConfigurarDependenciasDatabase(IServiceCollection services, IConfiguration config)
        {
            // services.AddDbContext<CatalogoContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddDbContext<CatalogoContext>(opt => opt.UseSqlServer(config.GetConnectionString(ConexaoSQL)));
        }

        private static void ConfigurarDependenciasRepository(IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
