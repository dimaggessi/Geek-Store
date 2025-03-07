using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Products.Commands.UpdateProduct;
public sealed class UpdateProductCommand : IRequest<Result<Product>>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Picture { get; set; }
    public string? Type { get; set; }
    public string? Brand { get; set; }
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
}
