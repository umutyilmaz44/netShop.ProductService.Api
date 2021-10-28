using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Exceptions;
using NetShop.ProductService.Application.Interfaces.Repository.Base;
using NetShop.ProductService.Application.Validators;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.Domain.Entities;

namespace NetShop.ProductService.Application.Features.Queries.BrandQueries
{
    public class GetBrandDetailQueryHandler : IRequestHandler<GetBrandDetailQuery, Response<BrandDto>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public GetBrandDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<BrandDto>> Handle(GetBrandDetailQuery request, CancellationToken cancellationToken)
        {            
            if (request == null || request.Id == Guid.Empty)
            {
                throw new BadRequestException($"{nameof(GetBrandDetailQuery)} request is null");
            }

            Brand entity = await this.unitOfWork.brandRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Brand), request.Id);
            }

            return new Response<BrandDto>(this.mapper.Map<BrandDto>(entity));
        }
    }
}