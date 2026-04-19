using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Promotions;

public class PromotionTranslationConfiguration : IEntityTypeConfiguration<PromotionTranslation>
{
    public void Configure(EntityTypeBuilder<PromotionTranslation> builder)
    {
        builder.Metadata.SetSchema("promotions");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
