using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Asp.Versioning;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Configure JWT Authentication (Matching Identity Service)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!)))
    };
});

builder.Services.AddAuthorization();

// Configure API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// MediatR scanning for CRM features
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(ZAP.Ecosystem.Application.CRM.Class1).Assembly);
});

builder.Services.AddDbContext<EcosystemDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem"));
});

// Configure Generic Repository
builder.Services.AddScoped(typeof(ZAP.Ecosystem.Shared.Data.IBaseRepository<>), typeof(ZAP.Ecosystem.Shared.Data.BaseRepository<>));
builder.Services.AddScoped<Microsoft.EntityFrameworkCore.DbContext>(provider => provider.GetRequiredService<EcosystemDbContext>());

// Override CRM Legacy Injection
builder.Services.AddScoped<ZAP.Ecosystem.Shared.Interfaces.ICurrentUserService, ZAP.Ecosystem.API.CRM.Mocks.MockUserService>();
builder.Services.AddScoped<CRM.Product.Domain.Interfaces.IProductRepository, ZAP.Ecosystem.API.CRM.Mocks.MockProductRepository>();
builder.Services.AddScoped<CRM.Product.Domain.Interfaces.IUnitRepository, ZAP.Ecosystem.API.CRM.Mocks.MockUnitRepository>();
builder.Services.AddScoped<CRM.Product.Domain.Interfaces.ICategoryRepository, ZAP.Ecosystem.API.CRM.Mocks.MockCategoryRepository>();
builder.Services.AddScoped<CRM.Product.Domain.Interfaces.IModifierGroupRepository, ZAP.Ecosystem.API.CRM.Mocks.MockModifierGroupRepository>();
builder.Services.AddScoped<CRM.Product.Domain.Interfaces.IBrandRepository, ZAP.Ecosystem.API.CRM.Mocks.MockBrandRepository>();

builder.Services.AddScoped<ZAP.Ecosystem.Application.CRM.Common.Interfaces.ICurrentUserService, MockCurrentUserService>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ILocationRepository, ZAP.Ecosystem.API.CRM.MockLocationRepository>();

// Auto-register all missing Domain Interfaces to the dynamic Proxy so handlers don't crash with 500 errors
var domainAssembly = typeof(ZAP.Ecosystem.Domain.CRM.ILocationRepository).Assembly;
var proxyMethod = typeof(ZAP.Ecosystem.API.CRM.MockRepositoryProxy).GetMethod(nameof(ZAP.Ecosystem.API.CRM.MockRepositoryProxy.Create));
foreach (var type in domainAssembly.GetTypes())
{
    if (type.IsInterface && type.Name.EndsWith("Repository") && type != typeof(ZAP.Ecosystem.Domain.CRM.ILocationRepository))
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

app.MapOpenApi();
app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapGet("/", (HttpContext ctx) => 
{
    ctx.Response.Redirect("index.html");
    return Task.CompletedTask;
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

public class MockCurrentUserService : ZAP.Ecosystem.Application.CRM.Common.Interfaces.ICurrentUserService
{
    // Mock user for testing without token setup
    public string? UserGuid => "a6b32eee-a14a-4cec-a070-e23b6ea234fb";
    public int LocaleId => 2; // Vietnamese etc.
}


