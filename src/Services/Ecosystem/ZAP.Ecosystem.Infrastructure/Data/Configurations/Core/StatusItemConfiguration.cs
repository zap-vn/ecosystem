using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Core;

public class StatusItemConfiguration : IEntityTypeConfiguration<StatusItem>
{
    public void Configure(EntityTypeBuilder<StatusItem> builder)
    {
        builder.ToTable("crm_statusitem", "core");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
