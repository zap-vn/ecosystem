using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Core;

public class StatusItemConfiguration : IEntityTypeConfiguration<StatusItem>
{
    public void Configure(EntityTypeBuilder<StatusItem> builder)
    {
        builder.Metadata.SetSchema("core");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
