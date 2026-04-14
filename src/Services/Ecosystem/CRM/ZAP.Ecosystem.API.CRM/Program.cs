using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ZAP.Ecosystem.Infrastructure.Data;
using ZAP.Ecosystem.Application.CRM.Behaviors;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

builder.Services.AddDbContext<EcosystemDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem"));
});

// Configure Generic Repository from Shared Module
builder.Services.AddScoped(typeof(ZAP.Ecosystem.Shared.Data.IBaseRepository<>), typeof(ZAP.Ecosystem.Shared.Data.BaseRepository<>));

builder.Services.AddValidatorsFromAssembly(typeof(ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Validators.GetCustomerListQueryValidator).Assembly);
builder.Services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddScoped<ZAP.Ecosystem.Application.CRM.Common.Interfaces.ICurrentUserService, MockCurrentUserService>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ILocationRepository,     ZAP.Ecosystem.Infrastructure.Repositories.CRM.LocationRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICustomerRepository,      ZAP.Ecosystem.Infrastructure.Repositories.CRM.CustomerRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IModifierGroupRepository, ZAP.Ecosystem.Infrastructure.Repositories.CRM.ModifierGroupRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICollectionRepository,    ZAP.Ecosystem.Infrastructure.Repositories.CRM.CollectionRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICategoryRepository,      ZAP.Ecosystem.Infrastructure.Repositories.CRM.CategoryRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IProductRepository,       ZAP.Ecosystem.Infrastructure.Repositories.CRM.ProductRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IModifierItemRepository,   ZAP.Ecosystem.Infrastructure.Repositories.CRM.ModifierItemRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IPromotionRepository,     ZAP.Ecosystem.Infrastructure.Repositories.CRM.PromotionRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IMenuRepository,          ZAP.Ecosystem.Infrastructure.Repositories.CRM.MenuRepository>();

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
