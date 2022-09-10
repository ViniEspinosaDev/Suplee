using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Suplee.Core.Communication.Mediator;
using Suplee.Core.Data.EventSourcing;
using Suplee.Core.Messages.CommonMessages.Notifications;
using Suplee.Core.Tools.Image;
using Suplee.EventSourcing;

namespace Suplee.Api.Configurations
{
    /// <summary>
    /// Injeta dependências do Core
    /// </summary>
    public static class CoreNativeInjector
    {
        /// <summary>
        /// Injeta dependências do Core
        /// </summary>
        public static IServiceCollection ConfigurarDependencias(IServiceCollection services, IConfiguration configuration)
        {
            ConfigurarDependenciasPadrao(services);

            services.AddScoped<IImageHelper, ImageHelper>();

            return services;
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
    }
}
