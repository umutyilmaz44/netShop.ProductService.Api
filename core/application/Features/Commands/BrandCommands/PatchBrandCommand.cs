using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.Domain.Common;

namespace NetShop.ProductService.Application.Features.Commands.BrandCommands
{
    public class PatchBrandCommand : BaseEntity, IRequest<Response>
    {
        public JsonPatchDocument<BrandDto> patchDto { get; set; }
        public ModelStateDictionary modelState { get; }

        public PatchBrandCommand(Guid id, JsonPatchDocument<BrandDto> patchDto, ModelStateDictionary modelState)
        {
            this.Id = id;
            this.patchDto = patchDto;
            this.modelState = modelState;
        }
    }
}