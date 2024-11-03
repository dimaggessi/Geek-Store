using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace GeekStore.Infrastructure.Services;
public sealed class PaymentService : IPaymentService
{
    private readonly IConfiguration _config;
    private readonly IShoppingCartRepository _cartRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IDeliveryService _deliveryService;

    public PaymentService(IConfiguration config, 
        IShoppingCartRepository cartRepository, 
        IUnityOfWork unityOfWork,
        IDeliveryService deliveryService)
    {
        _config = config;
        _cartRepository = cartRepository;
        _unityOfWork = unityOfWork;
        _deliveryService = deliveryService;
    }
    public async Task<ShoppingCart?> CreateOrUpdatePaymentIntent(string cartId)
    {
        StripeConfiguration.ApiKey = _config["StripeKeys:Secret"];

        var cart = await _cartRepository.GetCartAsync(cartId);

        if (cart is null)
            return null;

        var products = new List<Domain.Entities.Product>();

        #region CheckProductValues
        foreach (var item in cart.Items)
        {
            var productItem = await _unityOfWork
                .GetRepository<Domain.Entities.Product>()
                .GetByIdAsync(item.ProductId);

            if (productItem is null) return null;

            if (item.Price != productItem.Price)
            {
                item.Price = productItem.Price;
            }

            products.Add(productItem);
        }
        #endregion

        #region Shipping Price
        if (string.IsNullOrWhiteSpace(cart.PostalCode) || cart.DeliveryMethodId == 0)
            return null;

        var shippingPrice = 0m;
        var deliveryMethod = await _deliveryService.DeliveryMethods(products, 
            cart.PostalCode, cart.DeliveryMethodId);

        if (deliveryMethod is null) return null;

        shippingPrice = deliveryMethod[0].Price;
        #endregion

        #region StripePaymentIntent
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
        #endregion

        await _cartRepository.SetCartAsync(cart);

        return cart;
    }
}