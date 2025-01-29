using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using GeekStore.Infrastructure.Persistence;
using GeekStore.Infrastructure.Services;
using MediatR;

namespace GeekStore.Application.Payments.Commands;

public sealed record CreateOrUpdatePaymentIntentCommand : IRequest<Result<ShoppingCart>>
{
    public string? CartId { get; set; }
    public int? DeliveryMethodId { get; set; }
    public string? PostalCode { get; set; }

}

public class CreateOrUpdatePaymentIntentCommandHandler : IRequestHandler<CreateOrUpdatePaymentIntentCommand, Result<ShoppingCart>>
{
    private readonly IPaymentService _paymentService;
    private readonly IShoppingCartRepository _cartRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IDeliveryService _deliveryService;

    public CreateOrUpdatePaymentIntentCommandHandler(IPaymentService paymentService, 
        IShoppingCartRepository cartRepository, IUnityOfWork unityOfWork,
        IDeliveryService deliveryService)
    {
        _paymentService = paymentService;
        _cartRepository = cartRepository;
        _unityOfWork = unityOfWork;
        _deliveryService = deliveryService;
    }
    public async Task<Result<ShoppingCart>> Handle(CreateOrUpdatePaymentIntentCommand request, CancellationToken cancellationToken)
    {
        

        var cart = await _cartRepository.GetCartAsync(request.CartId!);

        if (cart is null)
            return Result.Failure<ShoppingCart>(new Error(
                ResourceErrorMessages.DEFAULT_NOT_FOUND,
                ResourceErrorMessages.SHOPPING_CART_NULL));

        var products = new List<Domain.Entities.Product>();

        #region CheckProductValues
        foreach (var item in cart.Items)
        {
            var productItem = await _unityOfWork
                .GetRepository<Domain.Entities.Product>()
                .GetByIdAsync(item.ProductId);

            if (productItem is null)
            {
                return Result.Failure<ShoppingCart>(new Error(
                    ResourceErrorMessages.DEFAULT_NOT_FOUND,
                    ResourceErrorMessages.PRODUCT_NOT_FOUND +
                        $"{item.ProductName},Id: {item.ProductId}"));
            }

            if (item.Price != productItem.Price)
            {
                item.Price = productItem.Price;
            }

            products.Add(productItem);
        }
        #endregion

        #region Shipping Price
        if (string.IsNullOrWhiteSpace(cart.PostalCode))
        {
            return Result.Failure<ShoppingCart>(new Error(
                ResourceErrorMessages.DEFAULT_NOT_FOUND,
                ResourceErrorMessages.EMPTY_POSTAL_CODE));
        }

        if (cart.DeliveryMethodId == 0)
        {
            return Result.Failure<ShoppingCart>(new Error(
                ResourceErrorMessages.DEFAULT_ERROR,
                ResourceErrorMessages.EMPTY_DELIVERY_METHOD_ID));
        }

        var shippingPrice = 0m;
        var deliveryMethod = await _deliveryService.DeliveryMethods(products,
            cart.PostalCode, cart.DeliveryMethodId);

        if (deliveryMethod is null)
        {
            return Result.Failure<ShoppingCart>(new Error(
                ResourceErrorMessages.DEFAULT_ERROR,
                ResourceErrorMessages.ERROR_DELIVERY_METHODS_NOT_FOUND));
        }

        shippingPrice = deliveryMethod[0].Price;
        #endregion

        ShoppingCart? updatedCart;

        try
        {
            updatedCart = await _paymentService.CreateOrUpdatePaymentIntent(cart, shippingPrice);
        }
        catch (Exception ex)
        {
            return Result.Failure<ShoppingCart>(new Error(ResourceErrorMessages.DEFAULT_ERROR, ResourceErrorMessages.ERROR_PAYMENT_INTENT));
        }

        if (updatedCart is null)
            return Result.Failure<ShoppingCart>(new Error(
                ResourceErrorMessages.DEFAULT_ERROR,
                ResourceErrorMessages.SHOPPING_CART_ERROR));

        return Result.Success(updatedCart);
    }
}
