using GeekStore.Application.Payments.Commands;
using MediatR;
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
        var request = new CreateOrUpdatePaymentIntentCommand
        {
            CartId = cartId
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
