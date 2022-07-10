using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Suplee.Catalogo.Api;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Suplee.Api.Configurations
{
    /// <summary>
    /// Configuração do Swagger
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Adiciona a configuração do Swagger
        /// </summary>
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                //c.OperationFilter<SwaggerDefaultValues>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                var assembly = Assembly.GetAssembly(typeof(Startup));

                var xmlFile = $"{assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        /// <summary>
        /// Usa a configuração do Swagger
        /// </summary>
        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            //app.UseMiddleware<SwaggerAuthorizedMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
            return app;
        }
    }

    /// <summary>
    /// Configura link do swagger
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="provider"></param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        /// <summary>
        /// Configura
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Version = $"V1",
                Title = $"Suplee API",
                Description = $"API interactive for the Suplee App",
                Contact = new OpenApiContact
                {
                    Name = "Suplee - Digital Team",
                    Email = "suplee.app@gmail.com",
                    Url = new Uri("https://suplee.vercel.app/")
                },
                License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
            };

            if (description.IsDeprecated)
            {
                info.Description += " Esta versão está obsoleta!";
            }

            return info;
        }
    }

    ///// <summary>
    ///// Swagger valores padrões
    ///// </summary>
    //public class SwaggerDefaultValues : IOperationFilter
    //{
    //    /// <summary>
    //    /// Aplica
    //    /// </summary>
    //    /// <param name="operation"></param>
    //    /// <param name="context"></param>
    //    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    //    {
    //        if (operation.Parameters == null)
    //        {
    //            return;
    //        }

    //        foreach (var parameter in operation.Parameters)
    //        {
    //            var description = context.ApiDescription
    //                .ParameterDescriptions
    //                .First(p => p.Name == parameter.Name);

    //            var routeInfo = description.RouteInfo;

    //            operation.Deprecated = OpenApiOperation.DeprecatedDefault;

    //            if (parameter.Description == null)
    //            {
    //                parameter.Description = description.ModelMetadata?.Description;
    //            }

    //            if (routeInfo == null)
    //            {
    //                continue;
    //            }

    //            if (parameter.In != ParameterLocation.Path && parameter.Schema.Default == null)
    //            {
    //                parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue.ToString());
    //            }

    //            parameter.Required |= !routeInfo.IsOptional;
    //        }
    //    }
    //}

    ///// <summary>
    ///// Autorização swagger middleware
    ///// </summary>
    //public class SwaggerAuthorizedMiddleware
    //{
    //    private readonly RequestDelegate _next;

    //    /// <summary>
    //    /// Construtor
    //    /// </summary>
    //    /// <param name="next"></param>
    //    public SwaggerAuthorizedMiddleware(RequestDelegate next)
    //    {
    //        _next = next;
    //    }

    //    /// <summary>
    //    /// Configura autorização
    //    /// </summary>
    //    /// <param name="context"></param>
    //    /// <returns></returns>
    //    public async Task Invoke(HttpContext context)
    //    {
    //        if (context.Request.Path.StartsWithSegments("/swagger")
    //            && !context.User.Identity.IsAuthenticated)
    //        {
    //            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    //            return;
    //        }

    //        await _next.Invoke(context);
    //    }
    //}
}
