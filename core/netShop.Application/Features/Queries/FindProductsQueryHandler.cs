using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using netShop.Application.Dtos;
using netShop.Application.Interfaces.Repository.Base;
using netShop.Application.Wrappers;
using netShop.Domain.Entities;
using netShop.Application.Validators;

namespace netShop.Application.Features.Queries
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
            FindProductsQueryValidator validator = new FindProductsQueryValidator();
            // var validationResult = ValidatorCheck.Validate<FindProductsQuery, PagedResponse<List<ProductDto>>>(validator, request);
            // if(validationResult != null)
            //     return (PagedResponse<List<ProductDto>>)validationResult;

            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new PagedResponse<List<ProductDto>>("Validation Error", validationResult.Errors.Select( x => $"{x.ErrorCode} : {x.ErrorMessage}").ToArray());
            }

            Expression<Func<Product, bool>> filter = PredicateBuilder.New<Product>(true);
            var original = filter;

            if (!string.IsNullOrEmpty(request.ProductCode))
                filter = filter.And(x => x.productCode.Contains(request.ProductCode));
            if (!string.IsNullOrEmpty(request.ProductName))
                filter = filter.And(x => x.productName.Contains(request.ProductName));
            if (!string.IsNullOrEmpty(request.Description))
                filter = filter.And(x => x.description.Contains(request.Description));

            if (filter == original)
                filter = x => true;

            PagedResponse<List<Product>> pagedEntities = await this.unitOfWork.productRepository.FindAsync(filter);
            PagedResponse<List<ProductDto>> pagedDtos = new PagedResponse<List<ProductDto>>(
                                                                    pagedEntities.Data.Select(entity => this.mapper.Map<ProductDto>(entity)).ToList(),
                                                                    pagedEntities.CurrentPage, pagedEntities.PageSize, pagedEntities.TotalCount);
            return pagedDtos;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}