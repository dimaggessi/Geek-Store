using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using GeekStore.Domain.Specifications;
using MediatR;

namespace GeekStore.Application.Products.Queries.GetProducts;
public class GetProductsQueryHandler 
    : IRequestHandler<GetProductsQuery, Result<PaginatedResult<IReadOnlyList<Product>>>>
{
    private readonly IUnityOfWork _unityOfWork;

    public GetProductsQueryHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }
    public async Task<Result<PaginatedResult<IReadOnlyList<Product>>>> Handle(GetProductsQuery request, 
        CancellationToken cancellationToken)
    {
        var specification = new ProductSpecification(request);
        var products = await _unityOfWork.GetRepository<Product>()
            .GetAllWithSpecificationAsync(specification, cancellationToken);

        if (products is null || products.Count <= 0)
        {
            return Result.Failure<PaginatedResult<IReadOnlyList<Product>>>(
                new Error("Error", "No products found."));
        }

        var response = new PaginatedResult<IReadOnlyList<Product>>
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            TotalCount = products.Count,
            Data = products
        };

        return Result.Success(response);
    }
}
