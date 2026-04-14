using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Locations;

public class LocationTypeTranslationConfiguration : IEntityTypeConfiguration<LocationTypeTranslation>
{
    public void Configure(EntityTypeBuilder<LocationTypeTranslation> builder)
    {
        builder.ToTable("crm_locationtypetranslation", "locations");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
