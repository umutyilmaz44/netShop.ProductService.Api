using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetShop.ProductService.Infrastructure.Persistence;
using Serilog;

namespace NetShop.ProductService.WebApi
{
    public class Program
    {
        private static IConfiguration configuration(IWebHostEnvironment hostEnv)
        {                
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            env = String.IsNullOrEmpty(env) ? hostEnv.EnvironmentName : env;
            return new ConfigurationBuilder()
                        .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                        .AddJsonFile($"Configurations/appsettings.json", optional: false)
                        .AddJsonFile($"Configurations/appsettings.{env}.json", optional: true)
                        .AddEnvironmentVariables()
                        .Build();
        }

        private static IConfiguration serilogConfiguration(IWebHostEnvironment hostEnv)
        {   
            string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            env = String.IsNullOrEmpty(env) ? hostEnv.EnvironmentName : env;

            return new ConfigurationBuilder()
                        .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                        .AddJsonFile($"Configurations/serilog.json", optional: false)
                        .AddJsonFile($"Configurations/serilog.{env}.json", optional: true)
                        .AddEnvironmentVariables()
                        .Build();
        }

        public static async Task Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args)
                    .Build();

                IWebHostEnvironment hostEnv = host.Services.GetRequiredService<IWebHostEnvironment>();
                        
                Log.Logger = new LoggerConfiguration()
                                .ReadFrom.Configuration(serilogConfiguration(hostEnv))
                                .CreateLogger();
                ILogger<Program> logger = host.Services.GetService<ILogger<Program>>();

                logger.LogDebug($"Environment['ASPNETCORE_ENVIRONMENT'] = {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
                logger.LogDebug($"IWebHostEnvironment['EnvironmentName'] = {hostEnv.EnvironmentName}");
                
                host = await host.MigrateDatabaseAsync(configuration(hostEnv),logger);
                host = await host.SeedDataAsync(configuration(hostEnv), logger);

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);
            
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) => {
                    var env = hostContext.HostingEnvironment;
                    config.SetBasePath(System.IO.Directory.GetCurrentDirectory())
                            .AddJsonFile($"Configurations/appsettings.json", optional: false)
                            .AddJsonFile($"Configurations/appsettings.{env.EnvironmentName}.json", optional: true)
                            .AddJsonFile($"Configurations/serilog.json", optional: false)
                            .AddJsonFile($"Configurations/serilog.{env.EnvironmentName}.json", optional: true)
                            .AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                            //.UseConfiguration(configuration)                            
                            .ConfigureLogging(c => c.ClearProviders())
                            .UseSerilog()
                            .UseStartup<Startup>();
                });
        }
    }
}
