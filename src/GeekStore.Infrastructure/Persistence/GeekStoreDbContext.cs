using GeekStore.Domain.Entities;
using GeekStore.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
public class GeekStoreDbContext(DbContextOptions<GeekStoreDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products{ get; set; }
    public DbSet<Address> Addresses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}