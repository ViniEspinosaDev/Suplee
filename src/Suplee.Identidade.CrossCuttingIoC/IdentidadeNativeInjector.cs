using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Suplee.Identidade.CrossCuttingIoC.Extensions;
using Suplee.Identidade.Data.Context;
using Suplee.Identidade.Domain.Models;
using System.Text;

namespace Suplee.Identidade.CrossCuttingIoC
{
    public static class IdentidadeNativeInjector
    {
        const string ConexaoSQL = "SqlConnection";

        public static void ConfigurarDependencias(IServiceCollection services, IConfiguration configuration)
        {
            ConfigurarDependenciasBancoDados(services, configuration);
            ConfigurarDependenciasJwtToken(services, configuration);
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

            services
                .AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AutenticacaoDbContext>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddDefaultTokenProviders();
        }
    }
}