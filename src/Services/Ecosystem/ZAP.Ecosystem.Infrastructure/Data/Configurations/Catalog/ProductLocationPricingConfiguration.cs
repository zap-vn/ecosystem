using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Catalog;

public class ProductLocationPricingConfiguration : IEntityTypeConfiguration<ProductLocationPricing>
{
    public void Configure(EntityTypeBuilder<ProductLocationPricing> builder)
    {
        builder.Metadata.SetSchema("catalog");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
