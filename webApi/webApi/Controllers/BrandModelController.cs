using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Features.Commands.BrandModelCommands;
using NetShop.ProductService.Application.Features.Queries.BrandModelQueries;
using NetShop.ProductService.Application.Parameters;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.WebApi.Controllers.Base;

namespace NetShop.ProductService.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class BrandModelsController : BaseController
    {
        /// <summary>
        ///   Find record by parameters
        /// </summary>
        /// <param name="request">filter by request fields</param>
        /// <param name="page">page number for pageable result</param>
        /// <param name="size">record number per page for pageable result</param>
        /// <param name="sort">sort result Ex: fieldName1 asc, fieldName2 dec vs...</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<List<BrandModelDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        [HttpPost]
        public async Task<ActionResult<PagedResponse<List<BrandModelDto>>>> Find([FromBody] FindBrandModelsQuery request, [FromQuery] int page = 0, int size = 10, string sort = "")
        {
            request.Page = page;
            request.PageSize = size;
            request.Sort = sort;
            var vm = await Mediator.Send(request);

            return Ok(vm);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        public async Task<ActionResult<Response<BrandModelDto>>> Get(Guid id)
        {
            var vm = await Mediator.Send(new GetBrandModelDetailQuery { Id = id });

            return Ok(vm);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        public async Task<ActionResult<Response<BrandModelDto>>> Create([FromBody] CreateBrandModelCommand request)
        {
            Response<BrandModelDto> response = await Mediator.Send(request);

            return Created("", response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        public async Task<ActionResult<Response>> Update([FromBody] UpdateBrandModelCommand request)
        {
            Response response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPatch("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        public async Task<ActionResult<Response>> Patch(Guid id, [FromBody] JsonPatchDocument<BrandModelDto> patchDto)
        {
            PatchBrandModelCommand request = new PatchBrandModelCommand(id, patchDto, ModelState);
            Response response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        public async Task<ActionResult<Response>> Delete(Guid id)
        {
            Response response = await Mediator.Send(new DeleteBrandModelCommand { Id = id });

            return Ok(response);
        }
    }
}