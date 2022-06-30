using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetShop.ProductService.Infrastructure.Persistence.Content;
using persistence.Settings;

namespace NetShop.ProductService.Infrastructure.Persistence
{
    public static class MigrationManager
    {
        public static async Task<IHost> MigrateDatabaseAsync(this IHost host, IConfiguration configuration)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {                    
                    try
                    {
                        DbSettings dbSettings = configuration.GetSection(nameof(DbSettings)).Get<DbSettings>();
                         if (!string.IsNullOrEmpty(dbSettings.DatabaseType) &&
                            string.Equals(dbSettings.DatabaseType, "Postgresql", StringComparison.InvariantCultureIgnoreCase))
                        {
                            var pendingMigrations = await appContext.Database.GetPendingMigrationsAsync();
                            if (pendingMigrations.Any())
                            {
                                Console.WriteLine($"You have {pendingMigrations.Count()} pending migrations to apply.");
                                Console.WriteLine("Applying pending migrations now");
                                await appContext.Database.MigrateAsync();
                            }

                            var lastAppliedMigration = (await appContext.Database.GetAppliedMigrationsAsync()).Last();

                            Console.WriteLine($"Application is on schema version: {lastAppliedMigration}");                    
                        }
                        else
                        {
                            await appContext.Database.EnsureCreatedAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            }
            return host;
        }
        
    }

}