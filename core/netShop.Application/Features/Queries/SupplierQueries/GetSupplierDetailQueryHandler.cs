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

namespace netShop.Application.Features.Queries.SupplierQueries
{
    public class GetSupplierDetailQueryHandler : IRequestHandler<GetSupplierDetailQuery, Response<SupplierDto>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public GetSupplierDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<SupplierDto>> Handle(GetSupplierDetailQuery request, CancellationToken cancellationToken)
        {            
            if (request == null || request.Id == Guid.Empty)
            {
                throw new BadRequestException($"{nameof(GetSupplierDetailQuery)} request is null");
            }

            Supplier entity = await this.unitOfWork.supplierRepository.GetByIdAsync(request.Id);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Supplier), request.Id);
            }

            return new Response<SupplierDto>(this.mapper.Map<SupplierDto>(entity));
        }
    }
}