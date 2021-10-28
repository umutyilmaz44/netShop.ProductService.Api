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

namespace NetShop.ProductService.Application.Features.Queries.SupplierQueries
{
    public class FindSuppliersQueryHandler : IRequestHandler<FindSuppliersQuery, PagedResponse<List<SupplierDto>>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public FindSuppliersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<PagedResponse<List<SupplierDto>>> Handle(FindSuppliersQuery request, CancellationToken cancellationToken)
        {            
            if (request == null)
            {
                throw new BadRequestException($"{nameof(FindSuppliersQuery)} request is null");
            }

            Expression<Func<Supplier, bool>> filter = PredicateBuilder.New<Supplier>(true);
            var original = filter;

            if (!string.IsNullOrEmpty(request.SupplierName))
                filter = filter.And(x => x.supplierName.Contains(request.SupplierName, StringComparison.InvariantCultureIgnoreCase));
            
            if (!string.IsNullOrEmpty(request.Description))
                filter = filter.And(x => x.description.Contains(request.Description, StringComparison.InvariantCultureIgnoreCase));
            
            if (!string.IsNullOrEmpty(request.Email))
                filter = filter.And(x => x.email.Contains(request.Email, StringComparison.InvariantCultureIgnoreCase));
            
            if (!string.IsNullOrEmpty(request.Fax))
                filter = filter.And(x => x.fax.Contains(request.Fax, StringComparison.InvariantCultureIgnoreCase));
            
            if (!string.IsNullOrEmpty(request.Phone))
                filter = filter.And(x => x.phone.Contains(request.Phone, StringComparison.InvariantCultureIgnoreCase));
            
            if (filter == original)
                filter = x => true;

            PagedResponse<IEnumerable<Supplier>> pagedEntities = await this.unitOfWork.supplierRepository.FindAsync(
                                                                        filter:filter, 
                                                                        orderBy: OrderableExtensions.GetOrderBy<Supplier>(request.Sort),
                                                                        pageIndex: request.Page, pageSize: request.PageSize);
            PagedResponse<List<SupplierDto>> pagedDtos = new PagedResponse<List<SupplierDto>>(
                                                                    pagedEntities.Data.Select(entity => this.mapper.Map<SupplierDto>(entity)).ToList(),
                                                                    pagedEntities.CurrentPage, pagedEntities.PageSize, pagedEntities.TotalCount);
            return pagedDtos;
        }
    }
}