using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Suplee.Catalogo.Api.Configurations.AutoMapper;
using Suplee.Catalogo.CrossCuttingIoC;
using Suplee.ExternalService.CrossCuttingIoC;
using Suplee.ExternalService.Imgbb.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Suplee.Catalogo.Api
{
    public class Startup
    {
        readonly string CorsPolicy = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAutoMapper(typeof(DomainToViewModelProfile), typeof(InputModelToDomainProfile));
            services.AddMediatR(typeof(Startup));

            services.Configure<ImgbbConfiguracao>(Configuration.GetSection("ExternalService:Imgbb"));

            NativeInjectionCatalogo.ConfigurarDependencias(services, Configuration);
            NativeInjectionExternalService.ConfigurarDependencias(services, Configuration);

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

            var ips = new List<string>()
                {
                    "https://supleeapiv1.herokuapp.com/",
                    "http://192.168.1.73:3000/",
                    "https://suplee.vercel.app/"
                };

            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                                  builder =>
                                  {
                                      builder
                                        .WithOrigins(ips.ToArray())
                                        .SetIsOriginAllowed(_ => true)
                                        .AllowAnyMethod()
                                        .AllowAnyHeader()
                                        .AllowCredentials();
                                  });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(CorsPolicy);

            app.UseHttpsRedirection();

            app.UseRouting();

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
