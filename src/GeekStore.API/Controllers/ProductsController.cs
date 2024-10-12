using GeekStore.Application.Products.Commands.CreateProduct;
using GeekStore.Application.Products.Commands.DeleteProduct;
using GeekStore.Application.Products.Commands.UpdateProduct;
using MediatR;
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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id,
            [FromBody] UpdateProductCommand request)
        {
            if (request.Id != id)
                return BadRequest("Url Id and Product Id mismatch");

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