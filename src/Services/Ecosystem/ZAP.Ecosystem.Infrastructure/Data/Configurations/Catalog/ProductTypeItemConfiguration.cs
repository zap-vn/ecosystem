using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Catalog;

public class ProductTypeItemConfiguration : IEntityTypeConfiguration<ProductTypeItem>
{
    public void Configure(EntityTypeBuilder<ProductTypeItem> builder)
    {
        builder.Metadata.SetSchema("catalog");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
