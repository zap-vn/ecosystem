using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Core;

public class TaxSyncSettingConfiguration : IEntityTypeConfiguration<TaxSyncSetting>
{
    public void Configure(EntityTypeBuilder<TaxSyncSetting> builder)
    {
        builder.ToTable("crm_taxsyncsetting", "core");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
