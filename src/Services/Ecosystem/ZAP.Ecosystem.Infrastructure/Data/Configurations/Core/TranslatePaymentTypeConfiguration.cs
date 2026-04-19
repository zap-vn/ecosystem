using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Core;

public class TranslatePaymentTypeConfiguration : IEntityTypeConfiguration<TranslatePaymentType>
{
    public void Configure(EntityTypeBuilder<TranslatePaymentType> builder)
    {
        builder.Metadata.SetSchema("core");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
