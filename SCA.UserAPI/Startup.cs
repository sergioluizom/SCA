﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SCA.Infraestrutura;
using SCA.Infraestrutura.Filter;
using SCA.Infraestrutura.Implementation;
using SCA.Infraestrutura.Interfaces;
using SCA.Infraestrutura.Middleware;
using SCA.Repository.Implementation;
using SCA.Repository.Interfaces;
using SCA.Service.Adapters.Interfaces;
using SCA.Service.Implementation;
using SCA.Service.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SCA
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration _configuration { get; }

        /// <summary>
        /// This method gets called by the runtime.Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.WriteIndented = true;
            });
            services.AddSingleton<Context>();
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "1.0",
                    Title = "SCA.UserAPI",
                    Description = "Aplicação SCA",
                });

                options.AddSecurityDefinition("apiKey", new OpenApiSecurityScheme
                {
                    Description = _configuration["keyValue"],
                    Name = _configuration["keyName"],
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "apiAuth"

                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "apiKey"
                    },
                    Scheme = "apiAuth",
                    In = ParameterLocation.Header
                },new List<string>()}});

                options.OperationFilter<AddAntiCsrfHeaderOperationFilter>();

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
            services.AddTransient<IAntiCSRFService, AntiCSRFService>();
            services.AddTransient<IRabbitMQ, Service.Adapters.Services.RabbitMQ>();

            services.AddScoped<IEquipamentoService, EquipamentoService>();
            services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var responseMessage = string.Empty;
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (exceptionHandlerFeature != null)
                    {
                        var logger = loggerFactory.CreateLogger("SCA API exception logger");
                        logger.LogError(500, exceptionHandlerFeature.Error, exceptionHandlerFeature.Error.Message);
                        responseMessage = exceptionHandlerFeature.Error.Message;
                    }

                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsync(responseMessage);
                });
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                string basePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
                options.SwaggerEndpoint($"{basePath}/swagger/v1.0/swagger.json", "SCA API");
            });

            app.UseMiddleware<AntiCSRFMiddleware>();
            app.UseMiddleware<KeyIdMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoins =>
            {
                endpoins.MapControllers();
            });
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
