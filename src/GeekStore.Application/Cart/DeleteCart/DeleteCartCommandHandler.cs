using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Cart.DeleteCart;

public sealed record DeleteCartCommand : IRequest<Result>
{
    public required string Id { get; init; }
}
public class DeleteCartCommandHandler(IShoppingCartRepository cartRepository)
    : IRequestHandler<DeleteCartCommand, Result>
{
    public async Task<Result> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var result = await cartRepository.DeleteCartAsync(request.Id);

        return result ?
            Result.Success(ResourceErrorMessages.GENERIC_SUCCESS_OPERATION) :
            Result.Failure(new Error(
                ResourceErrorMessages.DEFAULT_ERROR,
                ResourceErrorMessages.REMOVE_ERROR));
    }
}
