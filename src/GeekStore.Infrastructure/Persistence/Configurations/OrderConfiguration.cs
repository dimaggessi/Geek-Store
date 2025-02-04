using GeekStore.Domain.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Infrastructure.Persistence.Configurations;
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", "ecommerce");

        builder.OwnsOne(o => o.ShippingAddress, os => {
            os.WithOwner();
            os.Property(sa => sa.Name).HasMaxLength(50).IsRequired();
            os.Property(sa => sa.Number).HasMaxLength(50).IsRequired();
            os.Property(sa => sa.Street).HasMaxLength(100).IsRequired();
            os.Property(sa => sa.Neighborhood).HasMaxLength(100);
            os.Property(sa => sa.State).HasMaxLength(50).IsRequired();
            os.Property(sa => sa.City).HasMaxLength(50).IsRequired();
            os.Property(sa => sa.Country).HasMaxLength(50).IsRequired();
            os.Property(sa => sa.PostalCode).HasMaxLength(20).IsRequired();
        });

        builder.OwnsOne(o => o.DeliveryMethod, od =>
        {
            od.WithOwner();
            od.Property(dm => dm.Id).IsRequired();
            od.Property(dm => dm.Name).IsRequired().HasMaxLength(100);
            od.Property(dm => dm.Picture);
            od.Property(dm => dm.DeliveryTimeInDays);
            od.Property(dm => dm.Currency).IsRequired().HasMaxLength(10);
            od.Property(dm => dm.Price).IsRequired().HasColumnType("decimal(18,2)");
            od.Property(dm => dm.Discount).IsRequired().HasColumnType("decimal(18,2)");
        });

        builder.OwnsOne(o => o.PaymentSummary, op =>
        {
            op.WithOwner();
            op.Property(ps => ps.Brand).HasMaxLength(50).IsRequired();
        });

        builder.Property(o => o.OrderStatus).HasConversion(
            os => os.ToString(),
            os => (OrderStatus)Enum.Parse(typeof(OrderStatus), os)
        ).HasMaxLength(50).IsRequired();

        builder.Property(o => o.CustomerEmail).HasMaxLength(256).IsRequired();

        builder.Property(o => o.SubTotal).HasColumnType("decimal(18,2)");

        builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

        builder.Property(o => o.OrderDate).HasConversion(
            d => d.Kind == DateTimeKind.Local ? d.ToUniversalTime() : d,
            d => DateTime.SpecifyKind(d, DateTimeKind.Utc)
        );
    }
}
