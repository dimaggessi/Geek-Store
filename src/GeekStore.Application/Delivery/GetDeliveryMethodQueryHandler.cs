using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Delivery;

public sealed record GetDeliveryMethodQuery : IRequest<Result<List<DeliveryMethod>>>
{
    public string? CartId { get; set; }
    public int? DeliveryMethodId { get; set; }
    public string? PostalCode { get; set; }
}

public class GetDeliveryMethodQueryHandler : IRequestHandler<GetDeliveryMethodQuery, Result<List<DeliveryMethod>>>
{
    private readonly IDeliveryService _deliveryService;
    private readonly IUnityOfWork _unitOfWork;
    private readonly IShoppingCartRepository _cartRepository;

    public GetDeliveryMethodQueryHandler(IDeliveryService deliveryService, 
        IUnityOfWork unityOfWork, IShoppingCartRepository cartRepository)
    {
        _deliveryService = deliveryService;
        _unitOfWork = unityOfWork;
        _cartRepository = cartRepository;
    }
    public async Task<Result<List<DeliveryMethod>>> Handle(GetDeliveryMethodQuery request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetCartAsync(request.CartId!);

        if (cart is null)
            return Result.Failure<List<DeliveryMethod>>(new Error(
                ResourceErrorMessages.DEFAULT_NOT_FOUND,
                ResourceErrorMessages.SHOPPING_CART_NULL));

        if (!cart.Items.Any())
            return Result.Failure<List<DeliveryMethod>>(new Error(
                ResourceErrorMessages.DEFAULT_ERROR,
                ResourceErrorMessages.ERROR_EMPTY_CART_ITEMS));

        var products = new List<Product>();

        foreach (var item in cart.Items) 
        {
            var product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(item.ProductId, cancellationToken);

            if (product is null)
            {
                return Result.Failure<List<DeliveryMethod>>(new Error(
                    ResourceErrorMessages.DEFAULT_NOT_FOUND,
                    $"{ResourceErrorMessages.PRODUCT_NOT_FOUND}: {item.ProductName ?? "_"}, Id: {item.ProductId}"));
            }

            product.Quantity = item.Quantity;
            products.Add(product);
        }

        var response = await _deliveryService.DeliveryMethods(products, request.PostalCode!, request.DeliveryMethodId);

        if (response is null || !response.Any())
            return Result.Failure<List<DeliveryMethod>>(new Error(
                ResourceErrorMessages.DEFAULT_NOT_FOUND,
                ResourceErrorMessages.ERROR_DELIVERY_METHODS_NOT_FOUND));

        return Result.Success(response);
    }
}
