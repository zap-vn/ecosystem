using Microsoft.EntityFrameworkCore;

namespace ZAP.Ecosystem.HRM.Infrastructure.Data;

public class HRMDbContext : DbContext
{
    public HRMDbContext(DbContextOptions<HRMDbContext> options) : base(options) { }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HRMDbContext).Assembly);
    }
}
