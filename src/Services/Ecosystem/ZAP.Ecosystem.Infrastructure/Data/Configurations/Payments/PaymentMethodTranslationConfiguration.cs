using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Payments;

public class PaymentMethodTranslationConfiguration : IEntityTypeConfiguration<PaymentMethodTranslation>
{
    public void Configure(EntityTypeBuilder<PaymentMethodTranslation> builder)
    {
        builder.ToTable("crm_paymentmethodtranslation", "payments");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
