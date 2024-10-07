using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GeekStore.Domain.Entities;

namespace GeekStore.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "ecommerce");
            builder.Property(p => p.Id).IsRequired();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Description).IsRequired().HasMaxLength(300);
            builder.Property(p => p.Picture).IsRequired();
            builder.Property(p => p.Type).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Brand).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Quantity).IsRequired().HasDefaultValue(0);
            builder.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Width).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Height).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Length).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Weight).IsRequired().HasColumnType("decimal(18,2)");
        }
    }
}