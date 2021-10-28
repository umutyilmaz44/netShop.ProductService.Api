using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NetShop.ProductService.Application.Interfaces.Repository;
using NetShop.ProductService.Application.Interfaces.Repository.Base;
using NetShop.ProductService.Application.Interfaces.Context;
using NetShop.ProductService.Infrastructure.Persistence.Content;

namespace NetShop.ProductService.Infrastructure.Persistence.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private IApplicationDbContext applicationDbContext;
        public IProductRepository productRepository { get; private set; }

        public ISupplierRepository supplierRepository { get; private set; }

        public IBrandRepository brandRepository { get; private set; }

        public IBrandModelRepository brandModelRepository { get; private set; }

        public UnitOfWork(IApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            this.productRepository = new ProductRepository((ApplicationDbContext)this.applicationDbContext);
            this.supplierRepository = new SupplierRepository((ApplicationDbContext)this.applicationDbContext);
            this.brandRepository = new BrandRepository((ApplicationDbContext)this.applicationDbContext);
            this.brandModelRepository = new BrandModelRepository((ApplicationDbContext)this.applicationDbContext);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await ((ApplicationDbContext)this.applicationDbContext).Database.BeginTransactionAsync();
        }

        public async Task<int> CommitAsync()
        {
            return await this.applicationDbContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            ((ApplicationDbContext)this.applicationDbContext).Dispose();
        }
        public async ValueTask DisposeAsync()
        {
            await ((ApplicationDbContext)this.applicationDbContext).DisposeAsync();
        }
    }
}