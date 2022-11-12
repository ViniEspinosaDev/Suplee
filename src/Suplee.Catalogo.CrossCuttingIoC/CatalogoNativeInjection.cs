using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Suplee.Catalogo.Data;
using Suplee.Catalogo.Data.Repository;
using Suplee.Catalogo.Domain.Commands;
using Suplee.Catalogo.Domain.Events;
using Suplee.Catalogo.Domain.Interfaces;
using Suplee.Catalogo.Domain.Interfaces.Repositories;
using Suplee.Catalogo.Domain.Interfaces.Services;
using Suplee.Catalogo.Domain.Services;
using Suplee.Core.API.Enviroment;
using Suplee.Core.Messages.CommonMessages.IntegrationEvents;

namespace Suplee.Catalogo.CrossCuttingIoC
{
    public static class CatalogoNativeInjection
    {
        private static IEnvironment _environment;

        public static void ConfigurarDependencias(IEnvironment environment, IServiceCollection services, IConfiguration config)
        {
            _environment = environment;

            ConfigurarDependenciasDatabase(services, config);
            ConfigurarDependenciasRepository(services);
            ConfigurarDependenciasService(services);
            ConfigurarDependenciasCommand(services);
            ConfigurarDependenciasEvent(services);
        }

        private static void ConfigurarDependenciasEvent(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<ProdutoAdicionadoEvent>, CatalogoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoIniciadoEvent>, CatalogoEventHandler>();
            services.AddScoped<INotificationHandler<PedidoProcessamentoCanceladoEvent>, CatalogoEventHandler>();
            services.AddScoped<INotificationHandler<ProdutosEstoqueDebitadoEvent>, CatalogoEventHandler>();
            services.AddScoped<INotificationHandler<ProdutosEstoqueRepostoEvent>, CatalogoEventHandler>();
        }

        private static void ConfigurarDependenciasService(IServiceCollection services)
        {
            services.AddScoped<ICorreiosService, CorreiosService>();
            services.AddScoped<IImagemService, ImagemService>();
            services.AddScoped<IEstoqueService, EstoqueService>();
        }

        private static void ConfigurarDependenciasCommand(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<AdicionarProdutoCommand, bool>, CatalogoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarProdutoCommand, bool>, CatalogoCommandHandler>();
        }

        private static void ConfigurarDependenciasDatabase(IServiceCollection services, IConfiguration config)
        {
            // Configurar SQL
            services.AddDbContext<CatalogoContext>(opt => opt.UseSqlServer(_environment.ConexaoSQL));

            // Configurar MongoDb
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                return new MongoClient(_environment.ConexaoMongoDb);
            });

            services.AddScoped<IProdutoLeituraRepository, ProdutoLeituraRepository>();
        }

        private static void ConfigurarDependenciasRepository(IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
