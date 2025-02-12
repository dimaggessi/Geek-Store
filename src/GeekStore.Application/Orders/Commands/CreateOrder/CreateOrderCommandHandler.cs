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
        var orderedProductsList = new List<Product>();

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

            // Decrement quantity of Product Item - (Table Products)
            if (productItem.Quantity > item.Quantity)
            {
                productItem.Quantity -= item.Quantity;
            } 
            else
            {
                productItem.Quantity = 0;
            }

            // "Ordered Product" with new Cart Item quantity
            var orderedProduct = new Product
            {
                Quantity = item.Quantity,
                Id = productItem.Id,
                Name = productItem.Name,
                Width = productItem.Width,
                Height = productItem.Height,
                Length = productItem.Length,
                Weight = productItem.Weight,
                Description = productItem.Description,
                Picture = productItem.Picture,
                Type = productItem.Type,
                Brand = productItem.Brand,
            };

            orderedProductsList.Add(orderedProduct);

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

        var deliveryMethods = await _deliveryService.DeliveryMethods(orderedProductsList, 
            request.ShippingAddress!.PostalCode, cart.DeliveryMethodId);

        if (deliveryMethods.IsNullOrEmpty() || deliveryMethods.FirstOrDefault() == null)
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

        // Save Changes in data tables: Orders and Products
        if (await _unityOfWork.CommitAsync())
            return Result.Success(order);

        return Result.Failure<Order>(new Error(
            ResourceErrorMessages.DEFAULT_ERROR,
            ResourceErrorMessages.ERROR_ORDER_PERSIST));
    }
}
