using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.Domain.Common;

namespace NetShop.ProductService.Application.Features.Commands.BrandModelCommands
{
    public class PatchBrandModelCommand : BaseEntity, IRequest<Response>
    {
        public JsonPatchDocument<BrandModelDto> patchDto { get; set; }
        public ModelStateDictionary modelState { get; }

        public PatchBrandModelCommand(Guid id, JsonPatchDocument<BrandModelDto> patchDto, ModelStateDictionary modelState)
        {
            this.Id = id;
            this.patchDto = patchDto;
            this.modelState = modelState;
        }
    }
}