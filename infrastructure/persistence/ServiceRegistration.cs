using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetShop.ProductService.Infrastructure.Persistence.Content;
using NetShop.ProductService.Application.Interfaces.Context;
using NetShop.ProductService.Application.Interfaces.Repository.Base;
using NetShop.ProductService.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace NetShop.ProductService.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<ApplicationDbContext>(options => 
            //             options.UseNpgsql(configuration.GetConnectionString("DbConnection")));
            services.AddDbContext<ApplicationDbContext>(options => {
                        options.UseInMemoryDatabase(databaseName: "netShopDb")
                        .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                        // Logging sql 
                        // options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()) ;  
            services.AddScoped<IUnitOfWork, UnitOfWork>() ;   
        }
    }
}
