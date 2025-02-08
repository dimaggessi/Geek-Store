using GeekStore.API.Extensions;
using GeekStore.API.SignalR;
using GeekStore.Application.Cart.CreateCart;
using GeekStore.Application.Payments.Commands.CreateOrUpdatePaymentIntent;
using GeekStore.Application.Payments.Commands.UpdatePaymentStatus;
using GeekStore.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Stripe;

namespace GeekStore.API.Controllers;
public class PaymentsController : ApiController
{
    private readonly string _webHookSecret;
    private readonly ISender _mediator;
    private readonly ILogger<PaymentsController> _logger;
    private readonly IHubContext<NotificationHub> _hubContext;

    public PaymentsController(ISender mediator, 
        ILogger<PaymentsController> logger, IConfiguration config, IHubContext<NotificationHub> hubContext)
    {
        _mediator = mediator;
        _logger = logger;
        _webHookSecret = config["StripeKeys:WhSecret"]!;
        _hubContext = hubContext;
    }

    [Authorize]
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
            DeliveryMethodId = cart.DeliveryMethodId ?? 1,
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

    [HttpPost("Webhook")]
    public async Task<IActionResult> StripeWebhook()
    {

        using var reader = new StreamReader(Request.Body);

        var json = await reader.ReadToEndAsync();

        try
        {
            var stripeEvent = ConstructStripeEvent(json);

            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                if (stripeEvent.Data.Object is not PaymentIntent paymentIntent)
                    return BadRequest("Invalid Stripe event data");

                var request = new UpdatePaymentStatusCommand
                {
                    PaymentIntent = paymentIntent
                };

                var result = await _mediator.Send(request);
                
                if (result.IsFailure)
                {
                    return BadRequest(result.Error);
                }

                var connectionId = NotificationHub.GetConnectionIdByEmail(result.Value!.CustomerEmail);

                if (!string.IsNullOrEmpty(connectionId))
                {
                    await _hubContext.Clients.Client(connectionId)
                        .SendAsync("OrderCompleteNotification", result.Value.ToDto());
                }

            }

            _logger.LogInformation(stripeEvent.Type, "Stripe event type: ");
            return Ok();

        }
        catch (StripeException ex)
        {
            _logger.LogError(ex, "Stripe webhook error: ");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error in Stripe webhook");
            return StatusCode(500, "Internal server error");
        }
    }

    private Event ConstructStripeEvent(string json)
    {
        try
        {
            return EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _webHookSecret, throwOnApiVersionMismatch: false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to construct stripe event");
            throw new Exception("Invalid signature");
        }
    }


}
