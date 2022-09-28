using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlintaPoC.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration(appConfig =>
                {
                    appConfig.AddJsonFile($"appsettings.json", false, true);
                    appConfig.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", true, true);

                    var AppConfigConnectionString = "Endpoint=https://alintapoc-config.azconfig.io;Id=crjy-lf-s0:HyPbEvZEjy09DJJhjWtv;Secret=zJFlCFKHst9eqwNhc6BmVh9+DGt9g2+1PBHtNDV/SWE=";
                    appConfig.AddAzureAppConfiguration(options =>
                        options.Connect(AppConfigConnectionString)
                               .UseFeatureFlags()
                    );
                });
    }
}
