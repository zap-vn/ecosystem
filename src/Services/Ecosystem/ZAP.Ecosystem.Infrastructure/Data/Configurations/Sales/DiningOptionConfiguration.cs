using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Sales;

public class DiningOptionConfiguration : IEntityTypeConfiguration<DiningOption>
{
    public void Configure(EntityTypeBuilder<DiningOption> builder)
    {
        builder.Metadata.SetSchema("sales");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
