using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Application.Products.Commands.Common
{
    public record ProductCommandBase
    {
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required string Picture { get; init; }
        public required string Type { get; init; }
        public required string Brand { get; init; }
        public int Quantity { get; init; }
        public decimal Price { get; init; }
        public decimal Width { get; init; }
        public decimal Height { get; init; }
        public decimal Length { get; init; }
        public decimal Weight { get; init; }
    }
}