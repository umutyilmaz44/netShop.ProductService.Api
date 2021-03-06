using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace NetShop.ProductService.Application.Interfaces.Repository.Base {
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IProductRepository productRepository { get; }
        ISupplierRepository supplierRepository { get; }
        IBrandRepository brandRepository { get; }
        IBrandModelRepository brandModelRepository { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<int> CommitAsync(); 
    }
}