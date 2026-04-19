using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ZAP.Ecosystem.Domain.CRM;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Customers;

public class CustomerTranslationConfiguration : IEntityTypeConfiguration<CustomerTranslation>
{
    public void Configure(EntityTypeBuilder<CustomerTranslation> builder)
    {
        builder.Metadata.SetSchema("customers");
        // builder.HasKey(x => x.id); // Disabled due to ID naming variance
    }
}
