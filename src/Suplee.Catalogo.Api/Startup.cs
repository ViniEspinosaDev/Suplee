using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Suplee.Catalogo.Api.Configurations.AutoMapper;
using Suplee.Catalogo.CrossCuttingIoC;
using System;
using System.IO;
using System.Reflection;

namespace Suplee.Catalogo.Api
{
    public class Startup
    {
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
            NativeInjectionCatalogo.ConfigurarDependencias(services, Configuration);

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
