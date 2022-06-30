using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NetShop.ProductService.Infrastructure.Persistence.Content
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        private const string ConnectionStringName = "DbConnection";
        private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

        public TContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}..{0}webApi{0}webApi{0}configurations", Path.DirectorySeparatorChar);
            Console.WriteLine($"0- AspNetCoreEnvironmentg: '{ Environment.GetEnvironmentVariable(AspNetCoreEnvironment)}'.");
            
            return Create(basePath, Environment.GetEnvironmentVariable(AspNetCoreEnvironment));
        }

        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);

        private TContext Create(string basePath, string environmentName)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            string Host = configuration["DbSettings:Host"];
            string Port = configuration["DbSettings:Port"];
            string Username = configuration["DbSettings:Username"];
            string Password = configuration["DbSettings:Password"];
            string Database = configuration["DbSettings:Database"];
            string DatabaseType = configuration["DbSettings:DatabaseType"];
            bool isPostgresql = string.Equals(DatabaseType, "Postgresql", StringComparison.InvariantCultureIgnoreCase);
            var connectionString = $"Host={Host};Port={Port};Username={Username};Password={Password};Database={Database};";
            //configuration.GetConnectionString(ConnectionStringName);
            Console.WriteLine($"1- DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'. DatabaseType: '{DatabaseType}'");
            return Create(connectionString, isPostgresql);
        }

        private TContext Create(string connectionString, bool isPostgresql)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));
            }

            Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            if (isPostgresql)
            {
                Console.WriteLine($"2- DbConnection has stared for 'isPostgresql'");
                optionsBuilder.UseNpgsql(connectionString);
            }
            else
            {
                Console.WriteLine($"2- DbConnection has stared for 'InMemory'");
                optionsBuilder.UseInMemoryDatabase(databaseName: "netShopDb");
            }

            return CreateNewInstance(optionsBuilder.Options);
        }
    }
}