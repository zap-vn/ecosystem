using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Sales;

public class MenuHeaderConfiguration : IEntityTypeConfiguration<MenuHeader>
{
    public void Configure(EntityTypeBuilder<MenuHeader> builder)
    {
        builder.ToTable("crm_menuheader", "sales");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
