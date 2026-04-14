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
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ILocationRepository, ZAP.Ecosystem.API.CRM.MockLocationRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICustomerRepository, ZAP.Ecosystem.API.CRM.MockCustomerRepository>();

// Auto-register all missing Domain Interfaces to the dynamic Proxy so handlers don't crash with 500 errors
var domainAssembly = typeof(ZAP.Ecosystem.Domain.CRM.ILocationRepository).Assembly;
var proxyMethod = typeof(ZAP.Ecosystem.API.CRM.MockRepositoryProxy).GetMethod(nameof(ZAP.Ecosystem.API.CRM.MockRepositoryProxy.Create));
foreach (var type in domainAssembly.GetTypes())
{
    if (type.IsInterface && type.Name.EndsWith("Repository") && type != typeof(ZAP.Ecosystem.Domain.CRM.ILocationRepository) && type != typeof(ZAP.Ecosystem.Domain.CRM.ICustomerRepository))
    {
        builder.Services.AddScoped(type, sp => 
            proxyMethod!.MakeGenericMethod(type).Invoke(null, null)!
        );
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
