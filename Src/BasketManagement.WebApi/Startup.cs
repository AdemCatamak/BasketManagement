using System;
using System.IO;
using System.Reflection;
using BasketManagement.BasketModule.Infrastructure;
using BasketManagement.Shared.Infrastructure;
using BasketManagement.Shared.Infrastructure.MassTransitComponents;
using BasketManagement.StockModule.Infrastructure;
using BasketManagement.WebApi.Middleware;
using BasketManagement.WebApi.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BasketManagement.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                    options.SerializerSettings.StringEscapeHandling = StringEscapeHandling.Default;
                    options.SerializerSettings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full;
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                    options.SerializerSettings.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
                    options.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
                })
                .AddApplicationPart(typeof(HomeController).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo());

                c.CustomSchemaIds(type => type.ToString());
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSingleton<IIntegrationMessageConsumerAssembly, MassTransitConsumerAssembly>();
            CompositionRootRegisterer compositionRootRegisterer = new CompositionRootRegisterer(services, _configuration);
            compositionRootRegisterer.Registerer(new SharedCompositionRoot())
                .Registerer(new StockModuleCompositionRoot())
                .Registerer(new BasketModuleCompositionRoot())
                ;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<GeneralExceptionHandlerMiddleware>();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", ""); });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(builder => builder.MapControllers());
        }
    }
}