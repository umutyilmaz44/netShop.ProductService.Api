using Microsoft.EntityFrameworkCore;

namespace netShop.Persistence.Content
{
    public class ApplicationDbContextFactory: DesignTimeDbContextFactoryBase<ApplicationDbContext>
    {
        protected override ApplicationDbContext CreateNewInstance(DbContextOptions<ApplicationDbContext> options)
        {
            return new ApplicationDbContext(options);
        }
    }
}