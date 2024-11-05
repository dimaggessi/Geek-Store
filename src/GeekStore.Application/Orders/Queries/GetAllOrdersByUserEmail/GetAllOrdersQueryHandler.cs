﻿using GeekStore.Domain.Entities.OrderAggregate;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using GeekStore.Domain.Specifications;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace GeekStore.Application.Orders.Queries.GetAllOrdersByUserEmail;

public sealed record GetAllOrdersQuery : IRequest<Result<IReadOnlyList<Order>>>
{
    public string? CustomerEmail { get; init; }
}

public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result<IReadOnlyList<Order>>>
{
    private readonly IUnityOfWork _unitOfWork;
    public GetAllOrdersQueryHandler(IUnityOfWork unityOfWork)
    {
        _unitOfWork  = unityOfWork;
    }
    public async Task<Result<IReadOnlyList<Order>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
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
