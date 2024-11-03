namespace GeekStore.Domain.Entities.OrderAggregate;
public class ProductItemOrdered
{
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string Picture { get; set; }
}