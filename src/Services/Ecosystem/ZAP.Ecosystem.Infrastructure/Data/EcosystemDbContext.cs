using Microsoft.EntityFrameworkCore;

namespace ZAP.Ecosystem.Infrastructure.Data;

public class EcosystemDbContext : DbContext
{
    public DbSet<ZAP.Identity.Domain.Entities.Customer> Customers { get; set; }
    public DbSet<ZAP.Identity.Domain.Entities.CustomerMembership> CustomerMemberships { get; set; }
    public DbSet<ZAP.Identity.Domain.Entities.LoyaltyLevel> LoyaltyLevels { get; set; }

    public EcosystemDbContext(DbContextOptions<EcosystemDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Ensure schemas align with ZAP database hardening guidelines
        // e.g. modelBuilder.HasDefaultSchema("catalog");
        
        // Scan for all IEntityTypeConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcosystemDbContext).Assembly);
    }
}
