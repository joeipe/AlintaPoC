using AlintaPoC.API.Configurations;
using AlintaPoC.API.Messaging;
using AlintaPoC.Application.Services;
using AlintaPoC.Data;
using AlintaPoC.Data.Services;
using AlintaPoC.Integration.BlobStorage;
using AlintaPoC.Integration.RedisCache;
using AlintaPoC.Integration.TableStorage.Repositories;
using Azure.Data.Tables;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlintaPoC.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddHostedService<AzServiceBusConsumer>();

            if (Env.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddDistributedRedisCache(options =>
                {
                    options.Configuration = Configuration.GetConnectionString("RedisConnectionString");
                    options.InstanceName = "master";
                });
            }
            services.AddScoped<IAzFileStorage>(c => new AzFileStorage(Configuration.GetConnectionString("StorageConnectionString")));
            services.AddScoped(c => new TableServiceClient(Configuration.GetConnectionString("StorageConnectionString")));
            services.AddDbContext<DataContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DBConnectionString"))
                );
            services.AddScoped<Uow>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IAppService, AppService>();
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<CacheService>();
            services.AddAutoMapperSetup();

            services.AddCors(options =>
            {
                options.AddPolicy("AllRequests", builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(origin => origin == "http://localhost:3000")
                    .AllowCredentials();
                });
            });

            services.AddFeatureManagement();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AlintaPoC.API", Version = "v1" });
            });

            services
                .AddHealthChecks()
                .AddDbContextCheck<DataContext>()
                .AddAzureServiceBusTopicHealthCheck(Configuration.GetConnectionString("ServiceBusConnectionString"), "persontopic", "Person Topic", HealthStatus.Unhealthy);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlintaPoC.API v1"));

            app.UseCors("AllRequests");

            app.ConfigureCustomExceptionMiddleware();

            if (dataContext.Database.IsSqlServer())
            {
                var conStr = dataContext.Database.GetConnectionString();
                if (!conStr.Contains("TestAlintaPoCDb_Test"))
                {
                    dataContext.Database.Migrate();
                }
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultHealthChecks();
                endpoints.MapControllers();
            });
        }
    }
}
