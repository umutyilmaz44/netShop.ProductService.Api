using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetShop.ProductService.Domain.Entities;

namespace NetShop.ProductService.Application.Interfaces.Context
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