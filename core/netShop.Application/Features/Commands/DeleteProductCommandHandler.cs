using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using netShop.Application.Dtos;
using netShop.Application.Interfaces.Repository.Base;
using netShop.Domain.Common;
using netShop.Domain.Entities;

namespace netShop.Application.Features.Commands
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
        {
            IMapper mapper;
            IUnitOfWork unitOfWork;

            public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
            {
                this.mapper = mapper;
                this.unitOfWork = unitOfWork;
            }
            public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                await this.unitOfWork.productRepository.DeleteAsync(this.mapper.Map<Product>(request));
                await this.unitOfWork.CommitAsync();
                return Unit.Value;
            }
        }
}