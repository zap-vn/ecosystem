using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Inventory;

public class BomHeaderConfiguration : IEntityTypeConfiguration<BomHeader>
{
    public void Configure(EntityTypeBuilder<BomHeader> builder)
    {
        builder.Metadata.SetSchema("inventory");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
