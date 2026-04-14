using Microsoft.EntityFrameworkCore;

namespace ZAP.Ecosystem.Infrastructure.Data;

public class EcosystemDbContext : DbContext
{
    public DbSet<ZAP.Identity.Domain.Entities.Customer> Customers { get; set; }
    public DbSet<ZAP.Identity.Domain.Entities.CustomerMembership> CustomerMemberships { get; set; }
    public DbSet<ZAP.Identity.Domain.Entities.LoyaltyLevel> LoyaltyLevels { get; set; }
    
    // CRM Entities
    public DbSet<ZAP.Ecosystem.Domain.CRM.CustomerEntity> CrmCustomers { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.Location> Locations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.Product> Products { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.Category> Categories { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.Collection> Collections { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.MenuHeader> Menus { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ModifierGroup> ModifierGroups { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ModifierItem> ModifierItems { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.PromotionEntity> Promotions { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.StatusItem> StatusItems { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.StatusItemTranslation> StatusItemTranslations { get; set; }

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
