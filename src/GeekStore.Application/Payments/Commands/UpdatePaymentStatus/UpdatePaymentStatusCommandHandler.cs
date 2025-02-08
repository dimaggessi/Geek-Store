using GeekStore.Domain.Entities.OrderAggregate;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using GeekStore.Domain.Specifications;
using MediatR;
using Stripe;

namespace GeekStore.Application.Payments.Commands.UpdatePaymentStatus
{
    public sealed record UpdatePaymentStatusCommand : IRequest<Result<Order>>
    {
        public PaymentIntent? PaymentIntent { get; set; }
    }
    public class UpdatePaymentStatusCommandHandler : IRequestHandler<UpdatePaymentStatusCommand, Result<Order>>
    {
        private readonly IUnityOfWork _unityOfWork;

        public UpdatePaymentStatusCommandHandler(IUnityOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork;
        }
        public async Task<Result<Order>> Handle(UpdatePaymentStatusCommand request, CancellationToken cancellationToken)
        {
            var spec = new OrderSpecification(request.PaymentIntent!.Id, true);
            var order = await _unityOfWork.GetRepository<Order>().GetEntityWithSpecificationAsync(spec, cancellationToken);

            if (order is null)
            {
                return Result.Failure<Order>(new Error(
                    ResourceErrorMessages.DEFAULT_ERROR,
                    ResourceErrorMessages.ERROR_ORDER_NOT_FOUND));
            }

            if (request.PaymentIntent.Status is not "succeeded")
            {
                return Result.Failure<Order>(new Error(
                    ResourceErrorMessages.DEFAULT_ERROR,
                    ResourceErrorMessages.ERROR_PAYMENT_INVALID_STATUS));
            }

            if ((long)order.GetTotal() * 100 != request.PaymentIntent.Amount)
            {
                order.OrderStatus = OrderStatus.PaymentMismatch;
            }
            else
            {
                order.OrderStatus = OrderStatus.PaymentReceived;
            }

            var result = await _unityOfWork.CommitAsync(cancellationToken);

            return result ? 
                Result.Success(order) : 
                Result.Failure<Order>(new Error(
                ResourceErrorMessages.DEFAULT_ERROR, 
                ResourceErrorMessages.UNEXPECTED_ERROR));
        }
    }
}
