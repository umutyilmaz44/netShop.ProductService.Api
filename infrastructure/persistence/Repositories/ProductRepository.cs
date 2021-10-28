using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using NetShop.ProductService.Domain.Entities;
using NetShop.ProductService.Infrastructure.Persistence.Content;
using NetShop.ProductService.Infrastructure.Persistence.Repositories.Base;
using NetShop.ProductService.Application.Interfaces.Repository;
using NetShop.ProductService.Application.Wrappers;

namespace NetShop.ProductService.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Product>> GetProductByCode(string code)
        {
            PagedResponse<IEnumerable<Product>> pagedResponse = await this.FindAsync(x => x.productCode.Contains(code));
            return pagedResponse.Data;
        }
    }
}