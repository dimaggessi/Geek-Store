using GeekStore.Application.Brands.Queries.GetBrands;
using GeekStore.Application.Products.Commands.CreateProduct;
using GeekStore.Application.Products.Commands.DeleteProduct;
using GeekStore.Application.Products.Commands.UpdateProduct;
using GeekStore.Application.Products.Queries.GetProductById;
using GeekStore.Application.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.API.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly ISender _mediator;
        public ProductsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] GetProductsQuery request)
        {
            var result = await _mediator.Send(request);

            return result.Map<IActionResult>(
                onSuccess: response => Ok(new
                    {
                        Response = response
                    }),
                onFailure: error =>
                {
                    var errorResponse = new
                    {
                        Error = error,
                        ValidationErrors = result.ValidationErrors
                    };

                    return NotFound(errorResponse);
                }
            );
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById([FromRoute] GetProductByIdQuery request)
        {

            var result = await _mediator.Send(request);

            return result.Map<IActionResult>(
                onSuccess: product => Ok(product),
                onFailure: error =>
                {
                    var errorResponse = new
                    {
                        Error = error,
                        ValidationErrors = result.ValidationErrors
                    };

                    return NotFound(errorResponse);
                }
            );
        }

        [HttpGet("brands")]
        public async Task<IActionResult> GetBrands()
        {
            var request = new GetBrandsQuery();

            var result = await _mediator.Send(request);

            return result.Map<IActionResult>(
                onSuccess: brands => Ok(new
                {
                    Brands = brands
                }),
                onFailure: error =>
                {
                    var errorResponse = new
                    {
                        Error = error,
                        ValidationErrors = result.ValidationErrors
                    };

                    return NotFound(errorResponse);
                }
            );
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetTypes()
        {
            var request = new GetTypesQuery();

            var result = await _mediator.Send(request);

            return result.Map<IActionResult>(
                onSuccess: types => Ok(new
                {
                    Types = types
                }),
                onFailure: error =>
                {
                    var errorResponse = new
                    {
                        Error = error,
                        ValidationErrors = result.ValidationErrors
                    };

                    return NotFound(errorResponse);
                }
            );
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            var result = await _mediator.Send(request);

            return result.Map<IActionResult>(
                onSuccess: product => Ok(product),
                onFailure: error =>
                {
                    var errorResponse = new
                    {
                        Error = error,
                        ValidationErrors = result.ValidationErrors
                    };

                    return BadRequest(errorResponse);
                }
            );
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id,
            [FromBody] UpdateProductCommand request)
        {
            request.Id = id;

            var result = await _mediator.Send(request);

            return result.Map<IActionResult>(
                onSuccess: product => Ok(product),
                onFailure: error =>
                {
                    var errorResponse = new
                    {
                        Error = error,
                        ValidationErrors = result.ValidationErrors
                    };

                    return BadRequest(errorResponse);
                }
            );
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(
            [FromRoute] DeleteProductCommand request)
        {
            var result = await _mediator.Send(request);

            return result.Map<IActionResult>(
                onSuccess: product => Ok(product),
                onFailure: error => BadRequest(new { Error = error })
            );
        }
    }
}