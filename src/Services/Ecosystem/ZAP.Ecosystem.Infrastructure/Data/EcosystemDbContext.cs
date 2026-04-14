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
    
    // CRM Helper Entities for Navigation
    public DbSet<ZAP.Ecosystem.Domain.CRM.LocationTypeItem> LocationTypes { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.LocationTypeTranslation> LocationTypeTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductTypeItem> ProductTypes { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductTypeTranslation> ProductTypeTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductVariant> ProductVariants { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.PromotionTranslation> PromotionTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductCategoryMap> ProductCategoryMappings { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.LoyaltyTier> LoyaltyTiers { get; set; }

    public EcosystemDbContext(DbContextOptions<EcosystemDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Define composite keys for CRM join tables
        modelBuilder.Entity<ZAP.Ecosystem.Domain.CRM.CollectionItem>()
            .HasKey(c => new { c.collection_id, c.product_id });

        modelBuilder.Entity<ZAP.Ecosystem.Domain.CRM.ProductCategoryMap>()
            .HasKey(p => new { p.product_id, p.category_id });

        // Scan for all IEntityTypeConfiguration
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcosystemDbContext).Assembly);
    }
}
