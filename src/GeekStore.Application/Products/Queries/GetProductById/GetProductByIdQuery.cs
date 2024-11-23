using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeekStore.Application.Products.Queries.GetProductById;
public sealed class GetProductByIdQuery : IRequest<Result<Product>>
{
    [FromRoute]
    public int Id { get; set; }
}
