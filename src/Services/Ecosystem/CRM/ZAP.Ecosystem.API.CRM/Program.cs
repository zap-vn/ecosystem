using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ZAP.Ecosystem.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

builder.Services.AddDbContext<EcosystemDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem"));
});

// Configure Generic Repository from Shared Module
builder.Services.AddScoped(typeof(ZAP.Ecosystem.Shared.Data.IBaseRepository<>), typeof(ZAP.Ecosystem.Shared.Data.BaseRepository<>));

builder.Services.AddScoped<ZAP.Ecosystem.Application.CRM.Common.Interfaces.ICurrentUserService, MockCurrentUserService>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ILocationRepository,     ZAP.Ecosystem.API.CRM.MockLocationRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICustomerRepository,      ZAP.Ecosystem.API.CRM.MockCustomerRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IModifierGroupRepository, ZAP.Ecosystem.API.CRM.MockModifierGroupRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICollectionRepository,    ZAP.Ecosystem.API.CRM.MockCollectionRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICategoryRepository,      ZAP.Ecosystem.API.CRM.MockCategoryRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IProductRepository,       ZAP.Ecosystem.API.CRM.MockProductRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IModifierItemRepository,   ZAP.Ecosystem.API.CRM.MockModifierItemRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IPromotionRepository,     ZAP.Ecosystem.API.CRM.MockPromotionRepository>();

// Auto-register all remaining repository interfaces via DispatchProxy (returns empty data)
var domainAssembly = typeof(ZAP.Ecosystem.Domain.CRM.ILocationRepository).Assembly;
var proxyMethod = typeof(ZAP.Ecosystem.API.CRM.MockRepositoryProxy).GetMethod(nameof(ZAP.Ecosystem.API.CRM.MockRepositoryProxy.Create));
var manuallyRegistered = new HashSet<Type>
{
    typeof(ZAP.Ecosystem.Domain.CRM.ILocationRepository),
    typeof(ZAP.Ecosystem.Domain.CRM.ICustomerRepository),
    typeof(ZAP.Ecosystem.Domain.CRM.IModifierGroupRepository),
    typeof(ZAP.Ecosystem.Domain.CRM.ICollectionRepository),
    typeof(ZAP.Ecosystem.Domain.CRM.ICategoryRepository),
    typeof(ZAP.Ecosystem.Domain.CRM.IProductRepository),
    typeof(ZAP.Ecosystem.Domain.CRM.IModifierItemRepository),
    typeof(ZAP.Ecosystem.Domain.CRM.IPromotionRepository),
};
foreach (var type in domainAssembly.GetTypes())
{
    if (type.IsInterface && type.Name.EndsWith("Repository") && !manuallyRegistered.Contains(type))
    {
        builder.Services.AddScoped(type, _ =>
            proxyMethod!.MakeGenericMethod(type).Invoke(null, null)!);
    }
}

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(ZAP.Ecosystem.Application.CRM.Features.Categories.v1.Queries.GetCategoryListQuery).Assembly);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

public class MockCurrentUserService : ZAP.Ecosystem.Application.CRM.Common.Interfaces.ICurrentUserService
{
    // Mock user for testing without token setup
    public string? UserGuid => "a6b32eee-a14a-4cec-a070-e23b6ea234fb";
    public int LocaleId => 2; // Vietnamese etc.
}
