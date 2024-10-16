using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Products.Commands.DeleteProduct;
public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Product>>
{
    private readonly IUnityOfWork _unityOfWork;

    public DeleteProductCommandHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }
    public async Task<Result<Product>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unityOfWork.GetRepository<Product>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (product is null)
            return Result.Failure<Product>(new Error(
                ResourceErrorMessages.DEFAULT_NOT_FOUND, 
                ResourceErrorMessages.PRODUCT_NOT_FOUND));

        _unityOfWork.GetRepository<Product>().Remove(product);

        var result = await _unityOfWork.CommitAsync(cancellationToken);

        return result ?
            Result.Success(product) :
            Result.Failure<Product>(new Error(
                ResourceErrorMessages.DEFAULT_ERROR, 
                ResourceErrorMessages.ERROR_REMOVE_PRODUCT));
    }
}
