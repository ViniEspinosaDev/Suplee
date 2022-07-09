using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Suplee.Api.Configurations;
using Suplee.Catalogo.Api.Configurations.AutoMapper;
using Suplee.Catalogo.CrossCuttingIoC;
using Suplee.ExternalService.CrossCuttingIoC;
using Suplee.ExternalService.Imgbb.DTO;
using Suplee.Identidade.CrossCuttingIoC;
using System;
using System.IO;
using System.Reflection;

namespace Suplee.Catalogo.Api
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup's constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Appsettings.json
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// // This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(DomainToViewModelProfile), typeof(InputModelToDomainProfile));
            services.AddMediatR(typeof(Startup));

            services.Configure<ImgbbConfiguracao>(Configuration.GetSection("ExternalService:Imgbb"));

            services.ConfigurarDependencias();

            NativeInjectionCatalogo.ConfigurarDependencias(services, Configuration);
            NativeInjectionExternalService.ConfigurarDependencias(services, Configuration);
            IdentidadeNativeInjector.ConfigurarDependencias(services, Configuration);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = $"V1",
                    Title = $"Suplee - API",
                    Description = $"API interactive for the Suplee App",
                    Contact = new OpenApiContact
                    {
                        Name = "Suplee - Digital Team",
                        Email = "suplee.app@gmail.com",
                        Url = new Uri("https://suplee.vercel.app/")
                    }
                });

                var assembly = Assembly.GetAssembly(typeof(Startup));

                var xmlFile = $"{assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddCors();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalogo");
            });
        }
    }
}
