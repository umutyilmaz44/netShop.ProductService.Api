using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using netShop.Application.Interfaces.Repository;
using netShop.Application.Interfaces.Repository.Base;
using netShop.Application.Interfaces.Context;
using netShop.Persistence.Content;

namespace netShop.Persistence.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private IApplicationDbContext applicationDbContext;
        public IProductRepository productRepository { get; private set; }

        public UnitOfWork(IApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            this.productRepository = new ProductRepository((ApplicationDbContext)this.applicationDbContext);
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