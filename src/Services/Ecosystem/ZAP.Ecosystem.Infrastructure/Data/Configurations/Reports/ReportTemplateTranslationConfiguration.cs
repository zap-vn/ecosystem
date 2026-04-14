using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Reports;

public class ReportTemplateTranslationConfiguration : IEntityTypeConfiguration<ReportTemplateTranslation>
{
    public void Configure(EntityTypeBuilder<ReportTemplateTranslation> builder)
    {
        builder.ToTable("crm_reporttemplatetranslation", "reports");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
