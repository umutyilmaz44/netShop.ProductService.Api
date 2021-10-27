using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using netShop.Application.Dtos;
using netShop.Application.Features.Commands.BrandModelCommands;
using netShop.Application.Features.Queries.BrandModelQueries;
using netShop.Application.Parameters;
using netShop.Application.Wrappers;
using netShop.WebApi.Controllers.Base;

namespace netShop.WebApi.Controllers
{
    //[Authorize]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<PagedResponse<List<BrandModelDto>>>> Find([FromBody] FindBrandModelsQuery request, [FromQuery] int page = 0, int size = 10, string sort = "")
        {
            request.Page = page;
            request.PageSize = size;
            request.Sort = sort;
            var vm = await Mediator.Send(request);

            return base.Ok(vm);
        }

        [HttpGet("{id}")]
        // [AllowAnonymous]
        public async Task<ActionResult<BrandModelDto>> Get(Guid id)
        {
            var vm = await Mediator.Send(new GetBrandModelDetailQuery { Id = id });

            return base.Ok(vm);
        }

        [HttpPost]
        public async Task<ActionResult<Response<BrandModelDto>>> Create([FromBody] CreateBrandModelCommand request)
        {
            Response<BrandModelDto> response = await Mediator.Send(request);

            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody] UpdateBrandModelCommand request)
        {
            await Mediator.Send(request);

            return NoContent();
        }

        [HttpPatch("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<BrandModelDto> patchDto)
        {
            PatchBrandModelCommand request = new PatchBrandModelCommand(id, patchDto, ModelState);
            await Mediator.Send(request);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteBrandModelCommand { Id = id });

            return NoContent();
        }
    }
}