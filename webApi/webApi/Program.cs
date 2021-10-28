using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetShop.ProductService.Infrastructure.Persistence;
using Serilog;

namespace NetShop.ProductService.WebApi
{
    public class Program
    {
        private static String env = Environment.GetEnvironmentVariable("APSNETCORE_ENVIRONMENT");
        private static IConfiguration configuration
        {
            get
            {                
                return new ConfigurationBuilder()
                            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                            .AddJsonFile($"Configurations/appsettings.json", optional: false)
                            .AddJsonFile($"Configurations/appsettings.{env}.json", optional: true)
                            .AddEnvironmentVariables()
                            .Build();
            }
        }

        private static IConfiguration serilogConfiguration
        {
            get
            {                
                return new ConfigurationBuilder()
                            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                            .AddJsonFile($"Configurations/serilog.json", optional: false)
                            .AddJsonFile($"Configurations/serilog.{env}.json", optional: true)
                            .AddEnvironmentVariables()
                            .Build();
            }
        }

        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                .Build();

            Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(serilogConfiguration)
                            .CreateLogger();

            host.SeedData()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(configuration)
                              .UseStartup<Startup>()
                              .ConfigureLogging(c => c.ClearProviders())
                              .UseSerilog();
                });
        }
    }
}
