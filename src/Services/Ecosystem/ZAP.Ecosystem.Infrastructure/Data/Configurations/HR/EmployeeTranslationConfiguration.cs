using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.HR;

public class EmployeeTranslationConfiguration : IEntityTypeConfiguration<EmployeeTranslation>
{
    public void Configure(EntityTypeBuilder<EmployeeTranslation> builder)
    {
        builder.ToTable("crm_employeetranslation", "hr");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
