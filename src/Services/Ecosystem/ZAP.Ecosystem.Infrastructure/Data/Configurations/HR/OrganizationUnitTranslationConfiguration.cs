using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.HR;

public class OrganizationUnitTranslationConfiguration : IEntityTypeConfiguration<OrganizationUnitTranslation>
{
    public void Configure(EntityTypeBuilder<OrganizationUnitTranslation> builder)
    {
        builder.Metadata.SetSchema("hr");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
