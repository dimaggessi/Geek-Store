using GeekStore.Domain.Entities;
using GeekStore.Domain.Entities.OrderAggregate;
using GeekStore.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeekStore.Infrastructure.Persistence;
public class GeekStoreDbContext : IdentityDbContext<ApplicationUser>
{
    public GeekStoreDbContext(DbContextOptions<GeekStoreDbContext> options) : base(options)
    {
        
    }
    public DbSet<Product> Products{ get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("identity");
        builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}