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
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

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
            try
            {

                services.AddMvc().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                    options.JsonSerializerOptions.WriteIndented = true;
                });
                services.AddSingleton<Context>();
                services.AddControllers();
                Utils.Configuration.JwtConfig.AddIdentityConfiguration(services, _configuration);
                Utils.Configuration.SwaggerConfig.AddSwaggerConfiguration(services, XmlCommentsFilePath, "SCA Gerenciamento de Ativos");
                RegisterServices(services);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAntiCSRFService, AntiCSRFService>();
            services.AddScoped<IRabbitMQ, Service.Adapters.Services.RabbitMQ>();
            services.AddScoped<IEquipamentoService, EquipamentoService>();
            services.AddScoped<IManutencaoService, ManutencaoService>();
            services.AddScoped<IParadaService, ParadaService>();

            services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
            services.AddScoped<IManutencaoRepository, ManutencaoRepository>();
            services.AddScoped<IParadaRepository, ParadaRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseHttpsRedirection();
            app.UseRouting();
            Utils.Configuration.JwtConfig.UseIdentityConfiguration(app);
            Utils.Configuration.SwaggerConfig.UseSwaggerConfiguration(app);
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
