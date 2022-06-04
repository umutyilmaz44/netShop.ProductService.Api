using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetShop.ProductService.Application.Dtos;
using NetShop.ProductService.Application.Features.Commands.ProductCommands;
using NetShop.ProductService.Application.Features.Queries.ProductQueries;
using NetShop.ProductService.Application.Parameters;
using NetShop.ProductService.Application.Wrappers;
using NetShop.ProductService.WebApi.Controllers.Base;

namespace NetShop.ProductService.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class ProductsController : BaseController
    {
        public ProductsController(ILogger<ProductsController> logger) : base(logger)
        {
        }

        /// <summary>
        /// Find records by parameters
        /// </summary>
        /// <param name="request">filter by request fields</param>
        /// <param name="page">page number for pageable result</param>
        /// <param name="size">record number per page for pageable result</param>
        /// <param name="sort">sort result Ex: fieldName1 asc, fieldName2 dec vs...</param>
        [HttpPost, Route("find")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        public async Task<ActionResult<PagedResponse<List<ProductDto>>>> Find([FromBody] FindProductsQuery request, [FromQuery] int page = 0, int size = 10, string sort = "")
        {
            request.Page = page;
            request.PageSize = size;
            request.Sort = sort;
            var vm = await Mediator.Send(request);

            return Ok(vm);
        }

        /// <summary>
        /// Get record by id
        /// </summary>
        /// <param name="id">record unique id</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response))]
        public async Task<ActionResult<Response<ProductDto>>> Get(Guid id)
        {
            var vm = await Mediator.Send(new GetProductDetailQuery { Id = id });

            return Ok(vm);
        }

        /// <summary>
        /// Create new record by request parameter
        /// </summary>
        /// <param name="request">record data</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        public async Task<ActionResult<Response<ProductDto>>> Create([FromBody] CreateProductCommand request)
        {
            Response<ProductDto> response = await Mediator.Send(request);

            return Created("", response);
        }

        /// <summary>
        /// Update record by request parameters
        /// </summary>
        /// <param name="id">record unique id</param>
        /// <param name="request">all record data</param>
        [HttpPut("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        public async Task<ActionResult<Response>> Update(Guid id, [FromBody] UpdateProductCommand request)
        {
            request.Id = id;
            Response response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Patch record by request parameters
        /// </summary>
        /// <param name="id">record unique id</param>
        /// <param name="patchDto">partial record data</param>
        [HttpPatch("{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        public async Task<ActionResult<Response>> Patch(Guid id, [FromBody] JsonPatchDocument<ProductDto> patchDto)
        {
            PatchProductCommand request = new PatchProductCommand(id, patchDto, ModelState);
            Response response = await Mediator.Send(request);

            return Ok(response);
        }

        /// <summary>
        /// Delete record by id
        /// </summary>
        /// <param name="id">record unique id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response))]
        public async Task<ActionResult<Response>> Delete(Guid id)
        {
            Response response = await Mediator.Send(new DeleteProductCommand { Id = id });

            return Ok(response);
        }
    }
}