using GeekStore.Domain.Entities.OrderAggregate;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using GeekStore.Domain.Specifications;
using MediatR;

namespace GeekStore.Application.Payments.Commands.RefundOrder
{
    public sealed record RefundOrderCommand : IRequest<Result<Order>>
    {
        public int? OrderId { get; set; }
    }
    public class RefundOrderCommandHandler : IRequestHandler<RefundOrderCommand, Result<Order>>
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IPaymentService _paymentService;

        public RefundOrderCommandHandler(IUnityOfWork unityOfWork, IPaymentService paymentService)
        {
            _unityOfWork = unityOfWork;
            _paymentService = paymentService;
        }

        public async Task<Result<Order>> Handle(RefundOrderCommand request, CancellationToken cancellationToken)
        {
            var specification = new OrderSpecification((int)request.OrderId!);

            var order = await _unityOfWork.GetRepository<Order>()
                .GetEntityWithSpecificationAsync(specification, cancellationToken);

            if (order is null)
                return Result.Failure<Order>(new Error(
                    ResourceErrorMessages.DEFAULT_ERROR, 
                    ResourceErrorMessages.ERROR_ORDER_NOT_FOUND));

            if (order.OrderStatus is OrderStatus.Pending)
                return Result.Failure<Order>(new Error(
                    ResourceErrorMessages.DEFAULT_ERROR,
                    ResourceErrorMessages.ERROR_ORDER_STATUS));

            try
            {
                var result = await _paymentService.RefundPayment(order.PaymentIntentId);

                if (result is "succeeded")
                {
                    order.OrderStatus = OrderStatus.Refunded;
                }

                await _unityOfWork.CommitAsync();

                return Result.Success(order);
            }
            catch (Exception ex)
            {
                return Result.Failure<Order>(new Error(ResourceErrorMessages.DEFAULT_ERROR, ex.Message));
            }
        }
    }
}
