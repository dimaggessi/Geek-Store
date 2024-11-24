using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using GeekStore.Domain.Specifications;
using MediatR;

namespace GeekStore.Application.Brands.Queries.GetBrands;

public sealed record GetBrandsQuery : IRequest<Result<IReadOnlyList<string>>>
{
}

public class GetBrandsQueryHandler 
    : IRequestHandler<GetBrandsQuery, Result<IReadOnlyList<string>>>
{
    private readonly IUnityOfWork _unityOfWork;

    public GetBrandsQueryHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    async Task<Result<IReadOnlyList<string>>> IRequestHandler<GetBrandsQuery, Result<IReadOnlyList<string>>>.Handle(
        GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var specification = new BrandSpecification();
        var brands = await _unityOfWork.GetRepository<Product>()
            .GetAllWithSpecificationAsync(specification, cancellationToken);

        if (brands is null || brands.Count <= 0)
        {
            return Result.Failure<IReadOnlyList<string>>(new Error(
                ResourceErrorMessages.DEFAULT_ERROR, 
                ResourceErrorMessages.EMPTY_PRODUCT_BRAND));
        }

        return Result.Success(brands);
    }
}
