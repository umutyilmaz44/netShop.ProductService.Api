using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetShop.ProductService.Infrastructure.Persistence.Content;
using persistence.Settings;

namespace NetShop.ProductService.Infrastructure.Persistence
{
    public static class MigrationManager
    {
        public static async Task<IHost> MigrateDatabaseAsync(this IHost host, IConfiguration configuration, ILogger logger)
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
                                logger.LogDebug($"You have {pendingMigrations.Count()} pending migrations to apply.");
                                logger.LogDebug("Applying pending migrations now");
                                await appContext.Database.MigrateAsync();
                            }

                            var lastAppliedMigration = (await appContext.Database.GetAppliedMigrationsAsync()).Last();

                            logger.LogDebug($"Application is on schema version: {lastAppliedMigration}");                    
                        }
                        else
                        {
                            await appContext.Database.EnsureCreatedAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        logger.LogError(ex, "MigrateDatabaseAsync Error");
                        throw;
                    }
                }
            }
            return host;
        }
        
    }

}