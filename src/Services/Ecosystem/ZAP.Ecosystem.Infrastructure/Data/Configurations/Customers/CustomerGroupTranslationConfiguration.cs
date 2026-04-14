using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Customers;

public class CustomerGroupTranslationConfiguration : IEntityTypeConfiguration<CustomerGroupTranslation>
{
    public void Configure(EntityTypeBuilder<CustomerGroupTranslation> builder)
    {
        builder.ToTable("crm_customergrouptranslation", "customers");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
