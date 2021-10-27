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

namespace netShop.Application.Features.Queries.BrandModelQueries
{
    public class FindBrandModelsQueryHandler : IRequestHandler<FindBrandModelsQuery, PagedResponse<List<BrandModelDto>>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public FindBrandModelsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<List<BrandModelDto>>> Handle(FindBrandModelsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(FindBrandModelsQuery)} request is null");
            }

            Expression<Func<BrandModel, bool>> filter = PredicateBuilder.New<BrandModel>(true);
            var original = filter;

            if (!string.IsNullOrEmpty(request.ModelName))
                filter = filter.And(x => x.modelName.Contains(request.ModelName, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(request.Description))
                filter = filter.And(x => x.description.Contains(request.Description, StringComparison.InvariantCultureIgnoreCase));

            if (request.BrandId != Guid.Empty)
                filter = filter.And(x => x.brandId == request.BrandId);

            if (filter == original)
                filter = x => true;

            PagedResponse<IEnumerable<BrandModel>> pagedEntities = await this.unitOfWork.brandModelRepository.FindAsync(
                                                                        filter: filter,
                                                                        includes: (x => x.Include(u => u.brand)),
                                                                         orderBy: OrderableExtensions.GetOrderBy<BrandModel>(request.Sort),
                                                                        pageIndex: request.Page, pageSize: request.PageSize);
            PagedResponse<List<BrandModelDto>> pagedDtos = new PagedResponse<List<BrandModelDto>>(
                                                                    pagedEntities.Data.Select(entity => this.mapper.Map<BrandModelDto>(entity)).ToList(),
                                                                    pagedEntities.CurrentPage, pagedEntities.PageSize, pagedEntities.TotalCount);
            return pagedDtos;
        }
    }
}