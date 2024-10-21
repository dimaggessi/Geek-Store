using GeekStore.Application.Cart.CreateCart;
using GeekStore.Application.Cart.DeleteCart;
using GeekStore.Application.Cart.UpdateCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.API.Controllers;

public class CartController(ISender mediator) : ApiController
{
    [HttpGet]
    public async Task<IActionResult> GetCartById(string id)
    {
        // Id created by client side
        var request = new GetOrCreateCart { Id = id};
        var result = await mediator.Send(request);

        return result.Map<IActionResult>(
            onSuccess: cart => Ok(cart),
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

    [HttpPost]
    public async Task<IActionResult> UpdateCart(
        [FromBody] UpdateCartCommand cart)
    {

        var result = await mediator.Send(cart);

        return result.Map<IActionResult>(
            onSuccess: cart => Ok(cart),
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

    [HttpDelete]
    public async Task<ActionResult> DeleteCart(string id)
    {
        var request = new DeleteCartCommand { Id = id };
        var result = await mediator.Send(request);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        var errorResponse = new
        {
            Error = result.Error,
            ValidationErrors = result.ValidationErrors
        };

        return BadRequest(errorResponse);
    }

}
