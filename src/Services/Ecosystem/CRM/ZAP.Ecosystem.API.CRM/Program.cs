using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ZAP.Ecosystem.Infrastructure.Data;
using ZAP.Ecosystem.Application.CRM.Behaviors;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddDbContext<EcosystemDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem"));
});

// Generic repository
builder.Services.AddScoped(typeof(ZAP.Ecosystem.Shared.Data.IBaseRepository<>), typeof(ZAP.Ecosystem.Shared.Data.BaseRepository<>));

// FluentValidation + pipeline
builder.Services.AddValidatorsFromAssembly(typeof(ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Validators.GetCustomerListQueryValidator).Assembly);
builder.Services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(ZAP.Ecosystem.Application.CRM.Features.Categories.v1.Queries.GetCategoryListQuery).Assembly);
});

// Current user service (mock for dev)
builder.Services.AddScoped<ZAP.Ecosystem.Application.CRM.Common.Interfaces.ICurrentUserService, MockCurrentUserService>();

// Repository registrations
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ILocationRepository,      ZAP.Ecosystem.Infrastructure.Repositories.CRM.LocationRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICustomerRepository,       ZAP.Ecosystem.Infrastructure.Repositories.CRM.CustomerRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IModifierGroupRepository,  ZAP.Ecosystem.Infrastructure.Repositories.CRM.ModifierGroupRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICollectionRepository,     ZAP.Ecosystem.Infrastructure.Repositories.CRM.CollectionRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICategoryRepository,       ZAP.Ecosystem.Infrastructure.Repositories.CRM.CategoryRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IProductRepository,        ZAP.Ecosystem.Infrastructure.Repositories.CRM.ProductRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IModifierItemRepository,   ZAP.Ecosystem.Infrastructure.Repositories.CRM.ModifierItemRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IPromotionRepository,      ZAP.Ecosystem.Infrastructure.Repositories.CRM.PromotionRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IMenuRepository,           ZAP.Ecosystem.Infrastructure.Repositories.CRM.MenuRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IGeoCountryRepository,     ZAP.Ecosystem.Infrastructure.Repositories.CRM.GeoCountryRepository>();

var app = builder.Build();

app.MapHealthChecks("/health");

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
    public string? UserGuid => "a6b32eee-a14a-4cec-a070-e23b6ea234fb";
    public int LocaleId => 2;
}
