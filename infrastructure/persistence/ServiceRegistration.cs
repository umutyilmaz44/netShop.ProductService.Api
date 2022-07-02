using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetShop.ProductService.Infrastructure.Persistence.Content;
using NetShop.ProductService.Application.Interfaces.Context;
using NetShop.ProductService.Application.Interfaces.Repository.Base;
using NetShop.ProductService.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore.Diagnostics;
using persistence.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Serilog;

namespace NetShop.ProductService.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {            
            DbSettings dbSettings = configuration.GetSection(nameof(DbSettings)).Get<DbSettings>();

            Log.Debug($"{nameof(DbSettings)}: {Environment.NewLine}{JsonConvert.SerializeObject(dbSettings, Formatting.Indented)}");
            
            if(!string.IsNullOrEmpty(dbSettings.DatabaseType) && string.Equals(dbSettings.DatabaseType,"Postgresql", StringComparison.InvariantCultureIgnoreCase))
            {
                services.AddDbContext<ApplicationDbContext>(options => 
                            options.UseNpgsql(dbSettings.ConnectionString));
            } else 
            {           
                services.AddDbContext<ApplicationDbContext>(options => {
                            options.UseInMemoryDatabase(databaseName: dbSettings.Database)
                            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                            // Logging sql 
                            options.EnableDetailedErrors();
                            options.EnableSensitiveDataLogging();
                });
            }
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()) ;  
            services.AddScoped<IUnitOfWork, UnitOfWork>() ;   
        }
    }
}
