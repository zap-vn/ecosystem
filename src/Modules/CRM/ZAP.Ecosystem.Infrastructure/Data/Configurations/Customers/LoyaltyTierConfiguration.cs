using ZAP.Ecosystem.CRM.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ZAP.Ecosystem.Infrastructure.Data.Configurations.Customers;

public class LoyaltyTierConfiguration : IEntityTypeConfiguration<LoyaltyTier>
{
    public void Configure(EntityTypeBuilder<LoyaltyTier> builder)
    {
        builder.ToTable("loyalty_tier", "people");
        builder.HasKey(x => x.id);
        builder.Ignore(x => x.customers);  // do not map reverse nav
    }
}
