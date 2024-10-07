namespace GeekStore.Domain.Entities
{
    public class DeliveryMethod : BaseEntity
    {
        public required string Name { get; set; }
        public required string Picture { get; set; }
        public required string DeliveryTime { get; set; }
        public required string Currency { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}