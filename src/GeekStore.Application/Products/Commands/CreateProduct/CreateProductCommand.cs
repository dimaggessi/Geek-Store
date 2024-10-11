using GeekStore.Application.Products.Commands.Common;
using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Products.Commands.CreateProduct
{
    public sealed record CreateProductCommand() : ProductCommandBase, IRequest<Result<Product>>;
}