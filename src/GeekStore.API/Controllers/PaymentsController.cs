using GeekStore.Application.Cart.CreateCart;
using GeekStore.Application.Payments.Commands;
using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.API.Controllers;
public class PaymentsController : ApiController
{
    private readonly ISender _mediator;

    public PaymentsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("{cartId}")]
    public async Task<IActionResult> CreateOrUpdatePaymentIntent(string cartId)
    {
        var requestCart = new GetOrCreateCart { Id = cartId };
        var resultCart = await _mediator.Send(requestCart);
        var cart = resultCart.Value;

        if (cart is null)
            return NotFound(new
            {
                Error = new Error(
                    ResourceErrorMessages.DEFAULT_NOT_FOUND,
                    ResourceErrorMessages.SHOPPING_CART_NULL)
            });

        var request = new CreateOrUpdatePaymentIntentCommand
        {
            CartId = cartId,
            DeliveryMethodId = cart.DeliveryMethodId,
            PostalCode = cart.PostalCode,

        };

        var result = await _mediator.Send(request);

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
            });
    }
}
