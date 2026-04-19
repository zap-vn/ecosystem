using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Sales;

public class MenuItemHdConfiguration : IEntityTypeConfiguration<MenuItemHd>
{
    public void Configure(EntityTypeBuilder<MenuItemHd> builder)
    {
        builder.Metadata.SetSchema("sales");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
