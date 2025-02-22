using GeekStore.Domain.Entities.OrderAggregate;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using GeekStore.Domain.Specifications;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace GeekStore.Application.Orders.Queries.GetAllOrdersByUserEmail;

public sealed record GetAllOrdersByCustomerEmailQuery : IRequest<Result<IReadOnlyList<Order>>>
{
    public string? CustomerEmail { get; init; }
}

public class GetAllOrdersByCustomerEmailQueryHandler : IRequestHandler<GetAllOrdersByCustomerEmailQuery, Result<IReadOnlyList<Order>>>
{
    private readonly IUnityOfWork _unitOfWork;
    public GetAllOrdersByCustomerEmailQueryHandler(IUnityOfWork unityOfWork)
    {
        _unitOfWork  = unityOfWork;
    }
    public async Task<Result<IReadOnlyList<Order>>> Handle(GetAllOrdersByCustomerEmailQuery request, CancellationToken cancellationToken)
    {

        var spec = new OrderSpecification(request.CustomerEmail!);
        var orders = await _unitOfWork.GetRepository<Order>()
            .GetAllWithSpecificationAsync(spec, cancellationToken);

        if (orders.IsNullOrEmpty())
            return Result.Failure<IReadOnlyList<Order>>(new Error(
                ResourceErrorMessages.DEFAULT_NOT_FOUND, 
                ResourceErrorMessages.ERROR_ORDER_NOT_FOUND));

        return Result.Success(orders);
    }
}
