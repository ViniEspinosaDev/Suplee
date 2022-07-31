using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Suplee.Identidade.CrossCuttingIoC.Extensions;
using Suplee.Identidade.Data;
using Suplee.Identidade.Data.Context;
using Suplee.Identidade.Data.Repository;
using Suplee.Identidade.Domain.Autenticacao.Commands;
using Suplee.Identidade.Domain.Identidade.Commands;
using Suplee.Identidade.Domain.Identidade.Events;
using Suplee.Identidade.Domain.Interfaces;
using Suplee.Identidade.Domain.Models;
using System.Text;

namespace Suplee.Identidade.CrossCuttingIoC
{
    public static class IdentidadeNativeInjection
    {
        const string ConexaoSQL = "SqlConnection";

        public static void ConfigurarDependencias(IServiceCollection services, IConfiguration configuration)
        {
            ConfigurarDependenciasBancoDados(services, configuration);
            ConfigurarDependenciasJwtToken(services, configuration);
            ConfigurarDependenciasCommand(services);
            ConfigurarDependenciasRepository(services);
            ConfigurarDependenciasEvent(services);
        }

        private static void ConfigurarDependenciasEvent(IServiceCollection services)
        {
            services.AddScoped<INotificationHandler<UsuarioCadastradoEvent>, IdentidadeEventHandler>();
        }

        private static void ConfigurarDependenciasRepository(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }

        private static void ConfigurarDependenciasCommand(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CadastrarUsuarioCommand, Usuario>, IdentidadeCommandHandler>();
            services.AddScoped<IRequestHandler<EditarUsuarioCommand, bool>, IdentidadeCommandHandler>();
            services.AddScoped<IRequestHandler<ConfirmarCadastroCommand, bool>, AutenticacaoCommandHandler>();
            services.AddScoped<IRequestHandler<ReenviarEmailConfirmarCadastroCommand, string>, IdentidadeCommandHandler>();
            services.AddScoped<IRequestHandler<RecuperarSenhaCommand, string>, IdentidadeCommandHandler>();
            services.AddScoped<IRequestHandler<AlterarSenhaCommand, bool>, IdentidadeCommandHandler>();

            services.AddScoped<IRequestHandler<RealizarLoginEmailCommand, Usuario>, AutenticacaoCommandHandler>();
            services.AddScoped<IRequestHandler<RealizarLoginCPFCommand, Usuario>, AutenticacaoCommandHandler>();
        }

        private static void ConfigurarDependenciasJwtToken(IServiceCollection services, IConfiguration configuration)
        {
            var configuracaoAplicacao = configuration.GetSection("AppSettings");
            services.Configure<ConfiguracaoAplicacao>(configuracaoAplicacao);

            var appSettings = configuracaoAplicacao.Get<ConfiguracaoAplicacao>();
            var key = Encoding.ASCII.GetBytes(appSettings.Segredo);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.ValidoEm,
                    ValidIssuer = appSettings.Emissor
                };
            });
        }

        private static void ConfigurarDependenciasBancoDados(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AutenticacaoDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(ConexaoSQL)));
            services.AddDbContext<IdentidadeContext>(options => options.UseSqlServer(configuration.GetConnectionString(ConexaoSQL)));
            //services.AddDbContext<AutenticacaoDbContext>(options => options.UseInMemoryDatabase("database"));

            services
                .AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AutenticacaoDbContext>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddDefaultTokenProviders();
        }
    }
}