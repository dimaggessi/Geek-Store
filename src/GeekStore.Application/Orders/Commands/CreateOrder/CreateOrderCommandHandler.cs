using GeekStore.Domain.Entities;
using GeekStore.Domain.Entities.OrderAggregate;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace GeekStore.Application.Orders.Commands.CreateOrder;
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Order>>
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IShoppingCartRepository _cartRepository;
    private readonly IDeliveryService _deliveryService;

    public CreateOrderCommandHandler(IUnityOfWork unityOfWork, 
        IShoppingCartRepository cartRepository,
        IDeliveryService deliveryService)
    {
        _unityOfWork = unityOfWork;
        _cartRepository = cartRepository;
        _deliveryService = deliveryService;
    }
    public async Task<Result<Order>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {

        var cart = await _cartRepository.GetCartAsync(request.CartId!);

        if (cart is null)
            return Result.Failure<Order>(new Error(
                ResourceErrorMessages.DEFAULT_ERROR, 
                ResourceErrorMessages.CART_NOT_FOUND));

        if (cart.PaymentIntentId is null)
            return Result.Failure<Order>(new Error(
                ResourceErrorMessages.DEFAULT_ERROR,
                ResourceErrorMessages.EMPTY_PAYMENT_INTENT_ID));

        var items = new List<OrderItem>();
        var productsList = new List<Product>();

        foreach (var item in cart.Items)
        {
            var productItem = await _unityOfWork.GetRepository<Product>()
                .GetByIdAsync(item.ProductId);

            if (productItem is null)
            {
                return Result.Failure<Order>(new Error(
                    ResourceErrorMessages.DEFAULT_ERROR,
                    ResourceErrorMessages.ERROR_GET_PRODUCT));
            }

            // cart.Quantity of product item
            productItem.Quantity = item.Quantity;
            productsList.Add(productItem);

            var productItemOrdered = new ProductItemOrdered
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Picture = item.Picture,
            };

            var orderItem = new OrderItem
            {
                ItemOrdered = productItemOrdered,
                Price = productItem.Price,
                Quantity = item.Quantity,
            };

            items.Add(orderItem);
        }

        //-----------------------------------------------------
        // TODO: Check after frontend is created. (If this make sense)
        var deliveryMethods = await _deliveryService.DeliveryMethods(productsList, 
            request.ShippingAddress!.PostalCode, cart.DeliveryMethodId);
        //-----------------------------------------------------

        if (deliveryMethods.IsNullOrEmpty())
            return Result.Failure<Order>(new Error(
                    ResourceErrorMessages.DEFAULT_ERROR,
                    ResourceErrorMessages.ERROR_DELIVERY_METHOD_SELECT));

        var order = new Order
        {
            OrderItems = items,
            DeliveryMethod = deliveryMethods.FirstOrDefault()!,
            ShippingAddress = request.ShippingAddress,
            SubTotal = items.Sum(i => i.Price * i.Quantity),
            PaymentSummary = request.PaymentSummary!,
            PaymentIntentId = cart.PaymentIntentId,
            CustomerEmail = request.CustomerEmail!
        };

        // Order is created only when payment is succeeded
        _unityOfWork.GetRepository<Order>().Add(order);

        if (await _unityOfWork.CommitAsync())
            return Result.Success(order);

        return Result.Failure<Order>(new Error(
            ResourceErrorMessages.DEFAULT_ERROR,
            ResourceErrorMessages.ERROR_ORDER_PERSIST));
    }
}
