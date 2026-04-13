using Microsoft.EntityFrameworkCore;
using ZAP.Identity.Domain.Entities;

namespace ZAP.Identity.Infrastructure.Data;

public class IdentityDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<LoyaltyLevel> LoyaltyLevels { get; set; }
    public DbSet<CustomerMembership> CustomerMemberships { get; set; }

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options)

        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Setup schemas for identity data
        // e.g. modelBuilder.HasDefaultSchema("identity");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
    }
}
