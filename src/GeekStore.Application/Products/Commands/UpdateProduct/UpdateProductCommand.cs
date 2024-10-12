using GeekStore.Application.Products.Commands.Common;
using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Products.Commands.UpdateProduct;
public sealed record UpdateProductCommand : ProductCommandBase, IRequest<Result<Product>>
{
    public required int Id { get; init; }
}
