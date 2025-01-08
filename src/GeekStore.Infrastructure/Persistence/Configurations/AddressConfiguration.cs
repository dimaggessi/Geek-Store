using GeekStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeekStore.Infrastructure.Persistence.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses", "ecommerce");
            builder.Property(a => a.Number).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Street).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Neighborhood).HasMaxLength(100);
            builder.Property(a => a.City).IsRequired().HasMaxLength(50);
            builder.Property(a => a.State).IsRequired().HasMaxLength(50);
            builder.Property(a => a.PostalCode).IsRequired().HasMaxLength(20);
            builder.Property(a => a.Country).IsRequired().HasMaxLength(50);
        }
    }
}