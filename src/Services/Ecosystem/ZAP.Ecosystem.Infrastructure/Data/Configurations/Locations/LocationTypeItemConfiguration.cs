using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Locations;

public class LocationTypeItemConfiguration : IEntityTypeConfiguration<LocationTypeItem>
{
    public void Configure(EntityTypeBuilder<LocationTypeItem> builder)
    {
        builder.ToTable("crm_locationtypeitem", "locations");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
