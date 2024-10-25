namespace GeekStore.Application.Products.Commands.Common
{
    public record ProductCommandBase
    {
        public string? Name { get; init; }
        public string? Description { get; init; }
        public string? Picture { get; init; }
        public string? Type { get; init; }
        public string? Brand { get; init; }
        public int Quantity { get; init; }
        public decimal Price { get; init; }
        public decimal Width { get; init; }
        public decimal Height { get; init; }
        public decimal Length { get; init; }
        public decimal Weight { get; init; }
    }
}