using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Delivery;

public sealed record GetDeliveryMethodQuery : IRequest<Result<List<DeliveryMethod>>>
{
    public string? PostalCode { get; set; }
    public List<Product>? Products { get; set; }
}

public class GetDeliveryMethodQueryHandler : IRequestHandler<GetDeliveryMethodQuery, Result<List<DeliveryMethod>>>
{
    private readonly IDeliveryService _deliveryService;

    public GetDeliveryMethodQueryHandler(IDeliveryService deliveryService)
    {
        _deliveryService = deliveryService;
    }
    public async Task<Result<List<DeliveryMethod>>> Handle(GetDeliveryMethodQuery request, CancellationToken cancellationToken)
    {
        var response = await _deliveryService.DeliveryMethods(request.Products!, request.PostalCode!);

        if (response is null)
            return Result.Failure<List<DeliveryMethod>>(new Error(
                ResourceErrorMessages.DEFAULT_NOT_FOUND,
                ResourceErrorMessages.ERROR_DELIVERY_METHODS_NOT_FOUND));

        return Result.Success(response);
    }
}
