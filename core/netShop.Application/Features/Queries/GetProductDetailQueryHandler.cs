using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using netShop.Application.Dtos;
using netShop.Application.Interfaces.Repository.Base;
using netShop.Application.Wrappers;
using netShop.Domain.Entities;

namespace netShop.Application.Features.Queries
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
            Product entity = await this.unitOfWork.productRepository.GetByIdAsync(request.Id);

            return new Response<ProductDto>(this.mapper.Map<ProductDto>(entity));
        }
    }
}