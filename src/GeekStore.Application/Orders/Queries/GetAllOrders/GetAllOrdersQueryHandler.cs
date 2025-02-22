using GeekStore.Domain.Entities.OrderAggregate;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using GeekStore.Domain.Specifications;
using GeekStore.Domain.Specifications.SpecParams;
using MediatR;

namespace GeekStore.Application.Orders.Queries.GetAllOrders
{
    public sealed class GetAllOrdersQuery : OrderSpecParams, IRequest<Result<PaginatedResult<IReadOnlyList<Order>>>>
    {

    }

    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result<PaginatedResult<IReadOnlyList<Order>>>>
    {
        private readonly IUnityOfWork _unityOfWork;

        public GetAllOrdersQueryHandler(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        public async Task<Result<PaginatedResult<IReadOnlyList<Order>>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var specification = new OrderSpecification(request);

            var orders = await _unityOfWork.GetRepository<Order>().GetAllWithSpecificationAsync(specification, cancellationToken);
            var totalItems = await _unityOfWork.GetRepository<Order>().CountAsync(specification, cancellationToken);

            if (orders is null || !orders.Any())
            {
                return Result.Failure<PaginatedResult<IReadOnlyList<Order>>>(new Error(
                    ResourceErrorMessages.DEFAULT_ERROR, 
                    ResourceErrorMessages.ERROR_ORDER_NOT_FOUND));
            }

            var response = new PaginatedResult<IReadOnlyList<Order>>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalCount = totalItems,
                Data = orders
            };


            return Result.Success(response);
        }
    }
}
