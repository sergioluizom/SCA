﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using SCA.ApplicationService.Implementation;
using SCA.ApplicationService.Interfaces;
using SCA.Infraestrutura;
using SCA.Infraestrutura.Middleware;
using SCA.Repository.Implementation;
using SCA.Repository.Interfaces;
using SCA.Service.Implementation;
using SCA.Service.Interfaces;
using Swashbuckle.AspNetCore.Swagger;

namespace SCA
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
            });
            services.AddSingleton<Context>();
            
            services.AddSwaggerGen(options =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var environment = serviceProvider.GetRequiredService<IHostingEnvironment>();

                options.SwaggerDoc("v1.0", new Info()
                {
                    Version = "1.0",
                    Title = "SCA.API",
                    Description = "Aplicação SCA",
                });

                options.AddSecurityDefinition("apiKey", new ApiKeyScheme
                {
                    Description = _configuration["keyValue"],
                    Name = _configuration["keyName"],
                    In = "header"
                });
                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "apiKey", new string[] { } } });

                options.IncludeXmlComments(XmlCommentsFilePath);

                // Define que cada objeto do swagger possua o nome completo para evitar conflitos
                options.CustomSchemaIds(x => x.FullName);
            });
            RegisterServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<IAreaService, AreaService>();
            services.AddTransient<IAreaAppService, AreaAppService>();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                string basePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
                options.SwaggerEndpoint($"{basePath}/swagger/v1.0/swagger.json", "SCA API");
            });
            app.UseMiddleware<KeyIdMiddleware>();
            app.UseMvc();
        }

        private static string XmlCommentsFilePath {
            get {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}
