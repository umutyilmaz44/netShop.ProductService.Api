using System;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using netShop.Application.Dtos;
using netShop.Application.Wrappers;
using netShop.Domain.Common;

namespace netShop.Application.Features.Commands.BrandCommands
{
    public class PatchBrandCommand : BaseEntity, IRequest<Response<Unit>>
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