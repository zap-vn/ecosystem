using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Catalog;

public class ProductCategoryMapConfiguration : IEntityTypeConfiguration<ProductCategoryMap>
{
    public void Configure(EntityTypeBuilder<ProductCategoryMap> builder)
    {
        builder.ToTable("crm_productcategorymap", "catalog");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
