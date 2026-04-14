using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Payments;

public class PaymentTermsConfiguration : IEntityTypeConfiguration<PaymentTerms>
{
    public void Configure(EntityTypeBuilder<PaymentTerms> builder)
    {
        builder.ToTable("crm_paymentterms", "payments");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
