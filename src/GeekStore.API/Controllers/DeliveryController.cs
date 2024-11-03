using GeekStore.Application.Delivery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.API.Controllers;
public class DeliveryController : ApiController
{
    private readonly ISender _mediator;

    public DeliveryController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost()]
    public async Task<IActionResult> GetDeliveryMethods(
        [FromBody]GetDeliveryMethodQuerie request)
    {

        var result = await _mediator.Send(request);

        return result.Map<IActionResult>(
            onSuccess: deliveryList => Ok(deliveryList),
            onFailure: error =>
            {
                var responseError = new
                {
                    Error = error,
                    ValidationErrors = result.ValidationErrors
                };

                return BadRequest(responseError);
            });
    }
}
