using GeekStore.API.DTOs;
using GeekStore.API.Extensions;
using GeekStore.Application.Orders.Commands.CreateOrder;
using GeekStore.Application.Orders.Queries.GetAllOrdersByUserEmail;
using GeekStore.Application.Orders.Queries.GetOrderById;
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

    [HttpGet]
    public async Task<IActionResult> GetAllOrdersForUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var request = new GetAllOrdersByCustomerEmailQuery
        {
            CustomerEmail = email
        };

        var result = await _mediator.Send(request);

        return result.Map<IActionResult>(
            onSuccess: orders => Ok(orders.Select(order => order.ToDto()).ToList()),
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

    [HttpGet("{orderId:int}")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        var request = new GetOrderByIdAndCustomerEmailQuery
        {
            CustomerEmail = email,
            OrderId = orderId
        };
        
        var result = await _mediator.Send(request);

        return result.Map<IActionResult>(
            onSuccess: order => Ok(order.ToDto()),
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
    public async Task<IActionResult> CreateOrder(CreateOrderDto orderDto)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);

        if (email is null)
            return BadRequest(ResourceErrorMessages.ERROR_EMAIL_NOT_FOUND);

        var request = new CreateOrderCommand
        {
            CustomerEmail = orderDto.CustomerEmail,
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
            }
        );
    }
}
