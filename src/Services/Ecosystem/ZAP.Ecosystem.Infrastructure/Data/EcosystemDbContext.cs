using Microsoft.EntityFrameworkCore;

namespace ZAP.Ecosystem.Infrastructure.Data;

public class EcosystemDbContext : DbContext
{
    public DbSet<ZAP.Ecosystem.Domain.CRM.Brand> Brands { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.Category> Categories { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.Collection> Collections { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.CollectionItem> CollectionItems { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ModifierGroup> ModifierGroups { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.Product> Products { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductCategoryMap> ProductCategoryMaps { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductEntity> ProductEntities { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductLocationPricing> ProductLocationPricings { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductMedia> ProductMedias { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductTranslation> ProductTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductTypeItem> ProductTypeItems { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductVariant> ProductVariants { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.UomItem> UomItems { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.TaxSyncSetting> TaxSyncSettings { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.StatusItem> StatusItems { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.TranslatePaymentType> TranslatePaymentTypes { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.CustomerGroup> CustomerGroups { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.CustomerGroupTranslation> CustomerGroupTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.CustomerTranslation> CustomerTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.LoyaltyTier> LoyaltyTiers { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.Employee> Employees { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.EmployeeTranslation> EmployeeTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.OrganizationUnitTranslation> OrganizationUnitTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.BomHeader> BomHeaders { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.InventoryItem> InventoryItems { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.GeoProvince> GeoProvinces { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.GeoProvinceTranslation> GeoProvinceTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.Location> Locations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.LocationEntity> LocationEntities { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.LocationTypeItem> LocationTypeItems { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.LocationTypeTranslation> LocationTypeTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.Store> Stores { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.PaymentMethod> PaymentMethods { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.PaymentMethodTranslation> PaymentMethodTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.PaymentTerms> PaymentTermss { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.PaymentTermsTranslate> PaymentTermsTranslates { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.PaymentType> PaymentTypes { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.Promotion> Promotions { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.PromotionEntity> PromotionEntities { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.PromotionTranslation> PromotionTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ReportTemplate> ReportTemplates { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ReportTemplateTranslation> ReportTemplateTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.DiningOption> DiningOptions { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.MenuHeader> MenuHeaders { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.OrderDetailEntity> OrderDetailEntities { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.OrderEntity> OrderEntities { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.MenuItemHd> MenuItemHds { get; set; }
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

