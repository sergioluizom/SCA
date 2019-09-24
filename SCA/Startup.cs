using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using SCA.ApplicationService.Implementation;
using SCA.ApplicationService.Interfaces;
using SCA.Infraestrutura;
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<Context>();
            RegisterServices(services);
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
                
                options.AddSecurityDefinition(_configuration["keyName"], new ApiKeyScheme
                {
                    Description = _configuration["keyValue"],
                    Name = _configuration["keyName"],                    
                    In = "header"
                });
                options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { _configuration["keyName"], new string[] { } } });

                options.IncludeXmlComments(XmlCommentsFilePath);

                // Define que cada objeto do swagger possua o nome completo para evitar conflitos
                options.CustomSchemaIds(x => x.FullName);
            });

        }

        private void RegisterServices(IServiceCollection services)
        {
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

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                string basePath = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
                options.SwaggerEndpoint($"{basePath}/swagger/v1.0/swagger.json", "SCA.API");
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
