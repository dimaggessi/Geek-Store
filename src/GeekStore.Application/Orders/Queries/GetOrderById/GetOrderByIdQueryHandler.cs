using GeekStore.Domain.Entities.OrderAggregate;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using GeekStore.Domain.Specifications;
using MediatR;

namespace GeekStore.Application.Orders.Queries.GetOrderById;

public sealed record GetOrderByIdQuery : IRequest<Result<Order>>
{
    public int OrderId { get; init; }
}
public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, Result<Order>>
{
    private readonly IUnityOfWork _unitOfWork;
    public GetOrderByIdQueryHandler(IUnityOfWork unityOfWork)
    {
        _unitOfWork = unityOfWork;
    }
    public async Task<Result<Order>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {

        var spec = new OrderSpecification(request.OrderId);
        var order = await _unitOfWork.GetRepository<Order>()
            .GetEntityWithSpecificationAsync(spec, cancellationToken);

        if (order is null)
            return Result.Failure<Order>(new Error(
                ResourceErrorMessages.DEFAULT_NOT_FOUND, 
                ResourceErrorMessages.ERROR_ORDER_NOT_FOUND));

        return Result.Success(order);
    }
}
