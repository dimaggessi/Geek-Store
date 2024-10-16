using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Products.Queries.GetProductById;
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<Product>>
{
    private readonly IUnityOfWork _unityOfWork;

    public GetProductByIdQueryHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }
    public async Task<Result<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unityOfWork.GetRepository<Product>()
            .GetByIdAsync(request.Id, cancellationToken);

        if (product == null)
            return Result.Failure<Product>(new Error("Error", "Product not found."));

        return Result.Success<Product>(product);
    }
}
