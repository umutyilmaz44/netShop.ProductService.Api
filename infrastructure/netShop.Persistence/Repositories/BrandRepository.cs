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
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}