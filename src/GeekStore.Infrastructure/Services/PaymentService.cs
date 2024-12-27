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
    }
    public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(ShoppingCart cart, decimal shippingPrice)
    {
        StripeConfiguration.ApiKey = _config["StripeKeys:Secret"];

        var stripeService = new PaymentIntentService();

        PaymentIntent? intent = null;

        if (string.IsNullOrWhiteSpace(cart.PaymentIntentId))
        {
            var options = new PaymentIntentCreateOptions
            {
                // Multiplied by 100 because (conversion) to long type
                Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100))
                    + (long)shippingPrice * 100,
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
                Amount = (long)cart.Items.Sum(x => x.Quantity * (x.Price * 100))
                    + (long)shippingPrice * 100,
            };
            await stripeService.UpdateAsync(cart.PaymentIntentId, options);
        }

        var result = await _cartRepository.SetCartAsync(cart);

        return result;
    }
}