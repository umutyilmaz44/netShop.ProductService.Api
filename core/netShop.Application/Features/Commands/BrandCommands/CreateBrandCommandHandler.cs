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

namespace netShop.Application.Features.Commands.BrandCommands
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Response<BrandDto>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<BrandDto>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(CreateBrandCommand)} request is null");
            }

            Brand entity = null;

            using (var transaction = await this.unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    entity = await this.unitOfWork.brandRepository.AddAsync(this.mapper.Map<Brand>(request));
                    await this.unitOfWork.CommitAsync();

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    await transaction.RollbackAsync();
                }
            }

            return new Response<BrandDto>(this.mapper.Map<BrandDto>(entity));
        }
    }
}