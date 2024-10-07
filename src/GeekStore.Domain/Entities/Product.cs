namespace GeekStore.Domain.Entities
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Picture { get; set; }
        public required string Type { get; set; }
        public required string Brand { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal Length { get; set; }
        public decimal Weight { get; set; }
        
    }
}