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
    // TaxSyncSetting is a value object, not a DB entity - do not add DbSet
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
    public DbSet<ZAP.Ecosystem.Domain.CRM.GeoCountry> GeoCountries { get; set; }
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
    public DbSet<ZAP.Ecosystem.Domain.CRM.MenuHeader> Menus { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ModifierItem> ModifierItems { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.StatusItemTranslation> StatusItemTranslations { get; set; }

    // CRM Helper Entities for Navigation
    public DbSet<ZAP.Ecosystem.Domain.CRM.LocationTypeItem> LocationTypes { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductTypeItem> ProductTypes { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductTypeTranslation> ProductTypeTranslations { get; set; }
    public DbSet<ZAP.Ecosystem.Domain.CRM.ProductCategoryMap> ProductCategoryMappings { get; set; }

    public EcosystemDbContext(DbContextOptions<EcosystemDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ZAP.Ecosystem.Domain.CRM.CollectionItem>()
            .HasKey(c => new { c.collection_id, c.product_id });

        modelBuilder.Entity<ZAP.Ecosystem.Domain.CRM.ProductCategoryMap>()
            .HasKey(p => new { p.product_id, p.category_id });

        // TaxSyncSetting is a value object embedded in CustomerEntity, not a DB entity
        modelBuilder.Entity<ZAP.Ecosystem.Domain.CRM.CustomerEntity>(entity =>
        {
            entity.Ignore(c => c.TaxSyncSetting);
            // CustomerEntity has its own lowercase 'id' (Guid) mapped to DB; ignore the uppercase 'Id' inherited from BaseEntity
            entity.Ignore(c => c.Id);
            entity.HasKey(c => c.id);
        });

        // ProductLocationPricing has composite PK: product_variant_id + location_id
        modelBuilder.Entity<ZAP.Ecosystem.Domain.CRM.ProductLocationPricing>()
            .HasKey(p => new { p.product_variant_id, p.location_id });

        // OrderDetailEntity has complex properties that are not entities
        modelBuilder.Entity<ZAP.Ecosystem.Domain.CRM.OrderDetailEntity>(entity =>
        {
            entity.Ignore(e => e.OrderSummary);
            entity.Ignore(e => e.Shipping);
            entity.Ignore(e => e.PaymentList);
            entity.HasKey(e => e.Id);
        });

        // OrderEntity has complex properties that are not entities
        modelBuilder.Entity<ZAP.Ecosystem.Domain.CRM.OrderEntity>(entity =>
        {
            entity.Ignore(e => e.Items);
        });

        // Explicitly ignore supporting classes that are not intended to be entities
        modelBuilder.Ignore<ZAP.Ecosystem.Domain.CRM.OrderSummaryInfo>();
        modelBuilder.Ignore<ZAP.Ecosystem.Domain.CRM.OrderItemSnapshot>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EcosystemDbContext).Assembly);
    }
}
