using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace netShop.WebApi
{
    public class Program
    {
        private static IConfiguration Configuration
        {
            get
            {
                String env = Environment.GetEnvironmentVariable("APSNETCORE_ENVIRONMENT");
                return new ConfigurationBuilder()
                            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional:false)
                            .AddJsonFile($"appsettings.{env}.json", optional:true)
                            .AddEnvironmentVariables()
                            .Build();
            }
        }

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseConfiguration(Configuration);
                    webBuilder.UseStartup<Startup>();
                });

        
    }
}
