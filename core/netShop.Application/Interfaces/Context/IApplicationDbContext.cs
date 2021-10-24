using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using netShop.Domain.Entities;

namespace netShop.Application.Interfaces.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
        DbSet<Brand> Brands { get; set; }
        DbSet<BrandModel> BrandModels { get; set; }

        Task<int> SaveChangesAsync(CancellationToken calcellationToken = default);
    }
}