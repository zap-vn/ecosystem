using ZAP.Ecosystem.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace ZAP.Ecosystem.Infrastructure.Data;

public class EcosystemDbContext : DbContext
{
    public DbSet<CustomerGroup> CustomerGroups { get; set; }
    public DbSet<CustomerGroupTranslation> CustomerGroupTranslations { get; set; }
    public DbSet<CustomerTranslation> CustomerTranslations { get; set; }
    public DbSet<CustomerEntity> CrmCustomers { get; set; }
    public DbSet<LoyaltyTier> LoyaltyTiers { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<PromotionEntity> PromotionEntities { get; set; }
    public DbSet<PromotionTranslation> PromotionTranslations { get; set; }

    public EcosystemDbContext(DbContextOptions<EcosystemDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CustomerEntity>(entity =>
        {
            entity.Ignore(c => c.Id);
            entity.Ignore(c => c.CreatedAt);
            entity.Ignore(c => c.UpdatedAt);
            entity.Ignore(c => c.IsDeleted);
            entity.Ignore(c => c.UserGuid);
            entity.HasKey(c => c.id);
        });

        modelBuilder.Entity<PromotionEntity>(entity =>
        {
            entity.Ignore(e => e.Id);
            entity.HasKey(e => e.id);
        });

        modelBuilder.Entity<LoyaltyTier>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Ignore(e => e.customers);
        });

        var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.GetName().Name!.StartsWith("ZAP.") && a.GetName().Name!.Contains(".Domain"))
            .ToArray();

        var infraAssemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.GetName().Name!.StartsWith("ZAP.") && a.GetName().Name!.Contains(".Infrastructure"))
            .ToArray();

        foreach (var assembly in domainAssemblies)
        {
            var tableTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute), inherit: false).Length > 0);
            
            foreach (var type in tableTypes)
            {
                modelBuilder.Entity(type);
            }
        }

        foreach (var assembly in infraAssemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }
}





