using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace GeekStore.Infrastructure.Services;
public sealed class PaymentService : IPaymentService
{
    private readonly IConfiguration _config;
    private readonly IShoppingCartRepository _cartRepository;

    public PaymentService(IConfiguration config, 
        IShoppingCartRepository cartRepository, 
        IUnityOfWork unityOfWork,
        IDeliveryService deliveryService)
    {
        _config = config;
        _cartRepository = cartRepository;
        StripeConfiguration.ApiKey = _config["StripeKeys:Secret"];
    }


    public async Task<string> RefundPayment(string paymentIntentId)
    {
        var refundOptions = new RefundCreateOptions
        {
            PaymentIntent = paymentIntentId
        };

        var refundService = new RefundService();
        var result = await refundService.CreateAsync(refundOptions);

        return result.Status;
    }

    public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(ShoppingCart cart, decimal shippingPrice)
    {
        var stripeService = new PaymentIntentService();

        PaymentIntent? intent = null;

        var itemsTotal = cart.Items.Sum(x => x.Quantity * x.Price);
        var totalAmount = itemsTotal + shippingPrice;

        if (string.IsNullOrWhiteSpace(cart.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                // Amount in cents
                Amount = (long)Math.Round(totalAmount * 100, MidpointRounding.AwayFromZero),
                Currency = "brl",
                PaymentMethodTypes = ["card"]
            };
            intent = await stripeService.CreateAsync(options);
            cart.PaymentIntentId = intent.Id;
            cart.ClientSecret = intent.ClientSecret;
        }
        else
        {
            // Updating payment intent if already have one
            var options = new PaymentIntentUpdateOptions
            {
                Amount = (long)Math.Round(totalAmount * 100, MidpointRounding.AwayFromZero),
            };
            await stripeService.UpdateAsync(cart.PaymentIntentId, options);
        }

        var result = await _cartRepository.SetCartAsync(cart);

        return result;
    }
}