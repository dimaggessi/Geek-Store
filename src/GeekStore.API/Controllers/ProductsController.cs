using GeekStore.Application.Products.Commands.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
    }
}