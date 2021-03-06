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

namespace NetShop.ProductService.Application.Features.Queries.BrandModelQueries
{
    public class GetBrandModelDetailQueryHandler : IRequestHandler<GetBrandModelDetailQuery, Response<BrandModelDto>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public GetBrandModelDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<BrandModelDto>> Handle(GetBrandModelDetailQuery request, CancellationToken cancellationToken)
        {            
            if (request == null || request.Id == Guid.Empty)
            {
                throw new BadRequestException($"{nameof(GetBrandModelDetailQuery)} request is null");
            }

            BrandModel entity = await this.unitOfWork.brandModelRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(BrandModel), request.Id);
            }

            return new Response<BrandModelDto>(this.mapper.Map<BrandModelDto>(entity));
        }
    }
}