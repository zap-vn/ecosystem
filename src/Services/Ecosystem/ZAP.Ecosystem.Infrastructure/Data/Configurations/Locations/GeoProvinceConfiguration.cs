using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Locations;

public class GeoProvinceConfiguration : IEntityTypeConfiguration<GeoProvince>
{
    public void Configure(EntityTypeBuilder<GeoProvince> builder)
    {
        builder.Metadata.SetSchema("locations");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
