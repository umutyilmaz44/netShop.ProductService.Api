using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using netShop.Domain.Entities;
using netShop.Persistence.Content;
using netShop.Persistence.Repositories.Base;
using netShop.Application.Interfaces.Repository;
using netShop.Application.Wrappers;

namespace netShop.Persistence.Repositories
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