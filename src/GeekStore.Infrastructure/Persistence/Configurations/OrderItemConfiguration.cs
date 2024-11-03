using GeekStore.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Infrastructure.Persistence.Configurations;
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItems", "ecommerce");
        builder.OwnsOne(o => o.ItemOrdered, op =>
        {
            op.WithOwner();
            op.Property(i => i.ProductName).HasMaxLength(100).IsRequired();
            op.Property(i => i.Picture).IsRequired();
        });
        builder.Property(o => o.Price).HasColumnType("decimal(18,2)");
    }
}
