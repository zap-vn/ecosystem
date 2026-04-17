using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Shared.Entities;

namespace ZAP.Ecosystem.Sales.Infrastructure.Data;

public class SalesDbContext : DbContext
{
    public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }

    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderDetailEntity> OrderDetails { get; set; }
    public DbSet<DiningOption> DiningOptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
    }
}
