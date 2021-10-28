using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.Domain.Common;

namespace NetShop.ProductService.Application.Features.Commands.ProductCommands
{
    public class PatchProductCommand : BaseEntity, IRequest<Response>
    {
        public JsonPatchDocument<ProductDto> patchDto { get; set; }
        public ModelStateDictionary modelState { get; }

        public PatchProductCommand(Guid id, JsonPatchDocument<ProductDto> patchDto, ModelStateDictionary modelState)
        {
            this.Id = id;
            this.patchDto = patchDto;
            this.modelState = modelState;
        }
    }
}