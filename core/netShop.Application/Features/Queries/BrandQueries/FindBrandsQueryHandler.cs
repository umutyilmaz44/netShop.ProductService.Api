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
using netShop.Application.Exceptions;
using netShop.Application.Interfaces.Repository.Extensions;

namespace netShop.Application.Features.Queries.BrandQueries
{
    public class FindBrandsQueryHandler : IRequestHandler<FindBrandsQuery, PagedResponse<List<BrandDto>>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public FindBrandsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<List<BrandDto>>> Handle(FindBrandsQuery request, CancellationToken cancellationToken)
        {            
            if (request == null)
            {
                throw new BadRequestException($"{nameof(FindBrandsQuery)} request is null");
            }

            Expression<Func<Brand, bool>> filter = PredicateBuilder.New<Brand>(true);
            var original = filter;

            if (!string.IsNullOrEmpty(request.BrandName))
                filter = filter.And(x => x.brandName.Contains(request.BrandName, StringComparison.InvariantCultureIgnoreCase));
            
            if (!string.IsNullOrEmpty(request.Description))
                filter = filter.And(x => x.description.Contains(request.Description, StringComparison.InvariantCultureIgnoreCase));
            
            if (filter == original)
                filter = x => true;

            PagedResponse<IEnumerable<Brand>> pagedEntities = await this.unitOfWork.brandRepository.FindAsync(
                                                                    filter: filter,
                                                                    orderBy: OrderableExtensions.GetOrderBy<Brand>(request.Sort), 
                                                                    pageIndex: request.Page, pageSize: request.PageSize);
            PagedResponse<List<BrandDto>> pagedDtos = new PagedResponse<List<BrandDto>>(
                                                                    pagedEntities.Data.Select(entity => this.mapper.Map<BrandDto>(entity)).ToList(),
                                                                    pagedEntities.CurrentPage, pagedEntities.PageSize, pagedEntities.TotalCount);
            return pagedDtos;
        }
    }
}