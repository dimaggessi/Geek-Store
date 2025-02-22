using GeekStore.API.DTOs;
using GeekStore.API.Extensions;
using GeekStore.Application.Orders.Queries.GetAllOrders;
using GeekStore.Application.Orders.Queries.GetAllOrdersByUserEmail;
using GeekStore.Application.Orders.Queries.GetOrderById;
using GeekStore.Application.Payments.Commands.RefundOrder;
using GeekStore.Domain.Shared;
using GeekStore.Domain.Specifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GeekStore.API.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : ApiController
{
    private readonly ISender _mediator;

    public AdminController(ISender mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("orders")]
    public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQuery request)
    {
        var result = await _mediator.Send(request);

        return result.Map<IActionResult>(
            onSuccess: response => Ok(new PaginatedResult<IReadOnlyList<OrderDto>>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalCount = response.TotalCount,
                Data = response.Data!.Select(order => order.ToDto()).ToList()
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

    [HttpGet("orders/{orderId:int}")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {

        var request = new GetOrderByIdQuery
        {
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

    [HttpGet("orders/{email}")]
    public async Task<IActionResult> GetOrdersByEmail([FromRoute] string email)
    {

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

    [HttpPost("orders/refund/{id:int}")]
    public async Task<IActionResult> RefundOrder(int id)
    {
        var request = new RefundOrderCommand
        {
            OrderId = id
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

}
