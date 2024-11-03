using GeekStore.API.DTOs;
using GeekStore.Application.Orders.Commands.CreateOrder;
using GeekStore.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GeekStore.API.Controllers;
public class OrdersController : ApiController
{
    private readonly ISender _mediator;

    public OrdersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto orderDto)
    {
        
        var email = User.FindFirstValue(ClaimTypes.Email);

        if (email is null)
            return BadRequest(ResourceErrorMessages.ERROR_EMAIL_NOT_FOUND);

        var request = new CreateOrderCommand
        {
            CustomerEmail = email,
            CartId = orderDto.CartId,
            DeliveryMethodId = orderDto.DeliveryMethodId,
            ShippingAddress = orderDto.ShippingAddress,
            PaymentSummary = orderDto.PaymentSummary,
        };

        var result = await _mediator.Send(request);

        return result.Map<IActionResult>(
            onSuccess: order => Ok(order),
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
