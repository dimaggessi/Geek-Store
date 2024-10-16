using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using GeekStore.Domain.Specifications.SpecParams;
using MediatR;

namespace GeekStore.Application.Products.Queries.GetProducts;
public sealed class GetProductsQuery 
    : ProductSpecParams, IRequest<Result<PaginatedResult<IReadOnlyList<Product>>>>
{
}
