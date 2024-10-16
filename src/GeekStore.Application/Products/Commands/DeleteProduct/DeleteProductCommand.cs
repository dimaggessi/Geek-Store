﻿using GeekStore.Domain.Entities;
using GeekStore.Domain.Shared;
using MediatR;

namespace GeekStore.Application.Products.Commands.DeleteProduct;
public sealed record DeleteProductCommand : IRequest<Result<Product>>
{
    public required int Id { get; init; }
}