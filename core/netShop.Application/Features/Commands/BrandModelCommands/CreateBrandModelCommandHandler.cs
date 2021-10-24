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

namespace netShop.Application.Features.Commands.BrandModelCommands
{
    public class CreateBrandModelCommandHandler : IRequestHandler<CreateBrandModelCommand, Response<BrandModelDto>>
    {
        IMapper mapper;
        IUnitOfWork unitOfWork;

        public CreateBrandModelCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<Response<BrandModelDto>> Handle(CreateBrandModelCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new BadRequestException($"{nameof(CreateBrandModelCommand)} request is null");
            }

            BrandModel entity = null;

            using (var transaction = await this.unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    entity = await this.unitOfWork.brandModelRepository.AddAsync(this.mapper.Map<BrandModel>(request));
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

            return new Response<BrandModelDto>(this.mapper.Map<BrandModelDto>(entity));
        }
    }
}