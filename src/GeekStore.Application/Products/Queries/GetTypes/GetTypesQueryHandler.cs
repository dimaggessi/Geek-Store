using GeekStore.Domain.Entities;
using GeekStore.Domain.Interfaces;
using GeekStore.Domain.Shared;
using GeekStore.Domain.Specifications;
using MediatR;

namespace GeekStore.Application.Brands.Queries.GetBrands;

public sealed record GetTypesQuery : IRequest<Result<IReadOnlyList<string>>>
{
}

public class GetTypesQueryHandler 
    : IRequestHandler<GetTypesQuery, Result<IReadOnlyList<string>>>
{
    private readonly IUnityOfWork _unityOfWork;

    public GetTypesQueryHandler(IUnityOfWork unityOfWork)
    {
        _unityOfWork = unityOfWork;
    }

    async Task<Result<IReadOnlyList<string>>> IRequestHandler<GetTypesQuery, Result<IReadOnlyList<string>>>.Handle(
        GetTypesQuery request, CancellationToken cancellationToken)
    {
        var specification = new TypeSpecification();
        var types = await _unityOfWork.GetRepository<Product>()
            .GetAllWithSpecificationAsync(specification, cancellationToken);

        if (types is null || types.Count <= 0)
        {
            return Result.Failure<IReadOnlyList<string>>(new Error(
                ResourceErrorMessages.DEFAULT_ERROR, 
                ResourceErrorMessages.EMPTY_PRODUCT_TYPE));
        }

        return Result.Success(types);
    }
}
