using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace netShop.Application.Interfaces.Repository.Base {
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IProductRepository productRepository { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<int> CommitAsync(); 
    }
}