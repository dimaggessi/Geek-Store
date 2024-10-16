using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Products.Queries.GetProductById;
public sealed record GetProductByIdQuery : IRequest<Result<Product>>
{
    public int Id { get; init; }
}
