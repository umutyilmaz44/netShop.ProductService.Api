using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using netShop.Application.Dtos;
using netShop.Application.Exceptions;
using netShop.Application.Interfaces.Repository.Base;
using netShop.Application.Validators;
using netShop.Application.Wrappers;
using netShop.Domain.Entities;

namespace netShop.Application.Features.Queries.ProductQueries
{
    public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, Response<ProductDto>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public GetProductDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<ProductDto>> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {            
            if (request == null || request.Id == Guid.Empty)
            {
                throw new BadRequestException($"{nameof(GetProductDetailQuery)} request is null");
            }

            Product entity = await this.unitOfWork.productRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Product), request.Id);
            }

            return new Response<ProductDto>(this.mapper.Map<ProductDto>(entity));
        }
    }
}