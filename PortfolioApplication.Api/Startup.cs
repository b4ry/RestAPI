using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using NetEscapades.AspNetCore.SecurityHeaders;
using NLog;
using NLog.Config;
using PortfolioApplication.Api.Extensions;
using PortfolioApplication.Middlewares;
using PortfolioApplication.Middlewares.Errors;
using PortfolioApplication.Services.DatabaseContexts;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

namespace PortfolioApplication.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PortfolioApplicationLoggingDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("PortfolioLoggingDatabaseConnectionString")));
            services.AddDbContext<PortfolioApplicationDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("PortfolioDatabaseConnectionString"),
                migr => migr.MigrationsAssembly("PortfolioApplication.Migrations")));

            services.AddDistributedRedisCache(o =>
            {
                o.Configuration = Configuration.GetSection("RedisSettings")["RedisIP"];
                o.InstanceName = Configuration.GetSection("RedisSettings")["RedisInstanceName"];
            });

            services.AddCustomHeaders();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "PortfolioApplication Web API",
                        Description = "PortfolioApplication API exposing endpoints to manipulate application's business objects",
                        Version = "v1"
                    });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "PortfolioApplication.Api.xml");
                c.IncludeXmlComments(xmlPath);
            });

            ApplicationContainer = services.AddApplicationModules();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    serviceScope.ServiceProvider.GetService<PortfolioApplicationLoggingDbContext>().Database.EnsureCreated();
                    serviceScope.ServiceProvider.GetService<PortfolioApplicationDbContext>().Database.Migrate();
                    serviceScope.ServiceProvider.GetService<PortfolioApplicationDbContext>().SeedData();
                }
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCustomHeadersMiddleware(
                    new HeaderPolicyCollection()
                    .AddFrameOptionsSameOrigin()
                    .AddXssProtectionBlock()
                    .AddContentTypeOptionsNoSniff());
            
            app.UseCors("AllowSpecificOrigin");
            app.UseAutoMapper();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<PrepareHttpResponseMiddleware>();

            LogManager.Configuration.Install(new InstallationContext());

            app.UseMvc();
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PortfolioApplication API V1.0");
            });

            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}
