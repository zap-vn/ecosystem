using Microsoft.EntityFrameworkCore;

namespace ZAP.Ecosystem.Finance.Infrastructure.Data;

public class FinanceDbContext : DbContext
{
    public FinanceDbContext(DbContextOptions<FinanceDbContext> options) : base(options) { }

    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<PaymentTerms> PaymentTerms { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<ReportTemplate> ReportTemplates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceDbContext).Assembly);
    }
}
