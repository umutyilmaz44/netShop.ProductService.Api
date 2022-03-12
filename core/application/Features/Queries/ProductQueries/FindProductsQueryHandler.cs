using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Interfaces.Repository.Base;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.Domain.Entities;
using NetShop.ProductService.Application.Validators;
using NetShop.ProductService.Application.Exceptions;
using NetShop.ProductService.Application.Interfaces.Repository.Extensions;

namespace NetShop.ProductService.Application.Features.Queries.ProductQueries
{
    public class FindProductsQueryHandler : IRequestHandler<FindProductsQuery, PagedResponse<List<ProductDto>>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public FindProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<List<ProductDto>>> Handle(FindProductsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(FindProductsQuery)} request is null");
            }

            Expression<Func<Product, bool>> filter = PredicateBuilder.New<Product>(true);
            var original = filter;

            if (request.BrandModelId != Guid.Empty)
                filter = filter.And(x => x.brandModelId == request.BrandModelId);
            
            if (request.SupplierId != Guid.Empty)
                filter = filter.And(x => x.supplierId == request.SupplierId);

            if (!string.IsNullOrEmpty(request.ProductCode))
                filter = filter.And(x => x.productCode.Contains(request.ProductCode, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(request.ProductName))
                filter = filter.And(x => x.productName.Contains(request.ProductName, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(request.Description))
                filter = filter.And(x => x.description.Contains(request.Description, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(request.GenericQuery))
                filter = filter.And(x => x.productCode.Contains(request.GenericQuery, StringComparison.InvariantCultureIgnoreCase) ||
                                         x.productName.Contains(request.GenericQuery, StringComparison.InvariantCultureIgnoreCase) ||
                                         x.description.Contains(request.GenericQuery, StringComparison.InvariantCultureIgnoreCase));

            if (request.Price.HasValue)
                filter = filter.And(x => x.price == request.Price.Value);
            if (request.PriceLowerThan.HasValue)
                filter = filter.And(x => x.price <= request.PriceLowerThan.Value);
            if (request.PriceGreaterThan.HasValue)
                filter = filter.And(x => x.price >= request.PriceGreaterThan.Value);

            if (request.Quantity.HasValue)
                filter = filter.And(x => x.quantity == request.Quantity.Value);
            if (request.QuantityLowerThan.HasValue)
                filter = filter.And(x => x.quantity <= request.QuantityLowerThan.Value);
            if (request.QuantityGreaterThan.HasValue)
                filter = filter.And(x => x.quantity >= request.QuantityGreaterThan.Value);

            if (filter == original)
                filter = x => true;

            PagedResponse<IEnumerable<Product>> pagedEntities = await this.unitOfWork.productRepository.FindAsync(
                                                                    filter:filter, 
                                                                    includes: (x => x.Include(u => u.supplier)
                                                                                     .Include(t => t.brandModel).ThenInclude(bm => bm.brand)), 
                                                                    orderBy: OrderableExtensions.GetOrderBy<Product>(request.Sort),
                                                                    pageIndex: request.Page, pageSize: request.PageSize);
            PagedResponse<List<ProductDto>> pagedDtos = new PagedResponse<List<ProductDto>>(
                                                                    pagedEntities.Data.Select(entity => this.mapper.Map<ProductDto>(entity)).ToList(),
                                                                    pagedEntities.CurrentPage, pagedEntities.PageSize, pagedEntities.TotalCount);
            return pagedDtos;
        }
    }
}