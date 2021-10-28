using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetShop.ProductService.Application.Interfaces.Repository.Base;
using NetShop.ProductService.Domain.Entities;

namespace NetShop.ProductService.Application.Interfaces.Repository
{
    public interface IBrandRepository : IRepository<Brand>
    {
    }

}