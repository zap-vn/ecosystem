using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Asp.Versioning;
using ZAP.Ecosystem.Shared.Middlewares;
using Scalar.AspNetCore;
using ZAP.Ecosystem.CRM.Application.Behaviors;
using FluentValidation;
using MediatR;
using ZAP.Ecosystem.Sales.Infrastructure.Data;
using ZAP.Ecosystem.HRM.Infrastructure.Data;
using ZAP.Ecosystem.Finance.Infrastructure.Data;
using ZAP.Ecosystem.Inventory.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

// --- 1. Core Services ---
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
        {
            NamingStrategy = new Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy()
        };
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    })
    .AddApplicationPart(typeof(ZAP.CRM.Catalog.API.Controllers.ProductsController).Assembly)
    .AddApplicationPart(typeof(ZAP.Ecosystem.CRM.API.Controllers.CrmPingController).Assembly)
    .AddApplicationPart(typeof(ZAP.Ecosystem.Sales.API.Controllers.SalesPingController).Assembly)
    .AddApplicationPart(typeof(ZAP.Ecosystem.Inventory.API.Controllers.InventoryPingController).Assembly)
    .AddApplicationPart(typeof(ZAP.Ecosystem.HRM.API.Controllers.HRMPingController).Assembly)
    .AddApplicationPart(typeof(ZAP.Ecosystem.Finance.API.Controllers.FinancePingController).Assembly);

// Register Identity / Current User Service
builder.Services.AddScoped<ZAP.Ecosystem.Shared.Interfaces.ICurrentUserService, ZAP.Ecosystem.API.CRM.Services.CurrentUserService>();

// --- 2. Authentication & Authorization ---
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
        IssuerSigningKey = new SymmetricSecurityKey(System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))),
        ValidAlgorithms = new[] { Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256 }
    };
});
builder.Services.AddAuthorization();

// --- 3. API Versioning ---
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

// --- 4. Database & Repositories ---
builder.Services.AddDbContext<EcosystemDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem"));
});

// Register Module-Specific DbContexts (sharing same connection)
builder.Services.AddDbContext<SalesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem")));

builder.Services.AddDbContext<HRMDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem")));

builder.Services.AddDbContext<FinanceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem")));




// Generic repository
builder.Services.AddScoped(typeof(ZAP.Ecosystem.Shared.Data.IBaseRepository<>), typeof(ZAP.Ecosystem.Shared.Data.BaseRepository<>));
builder.Services.AddScoped<Microsoft.EntityFrameworkCore.DbContext>(provider => provider.GetRequiredService<EcosystemDbContext>());

// Dynamic Repository & MediatR Scanning
var assemblies = new[]
{
    typeof(ZAP.Ecosystem.CRM.Domain.Entities.Customers.CustomerEntity).Assembly,
    typeof(ZAP.Ecosystem.Infrastructure.Data.EcosystemDbContext).Assembly,
    typeof(ZAP.Ecosystem.Shared.Responses.CrmResponse).Assembly,
    // CRM Module
    typeof(ZAP.CRM.Catalog.Domain.Entities.Brands.Brand).Assembly,
    typeof(ZAP.CRM.Catalog.Application.DependencyInjection).Assembly,
    typeof(ZAP.CRM.Catalog.Infrastructure.Repositories.Products.ProductRepository).Assembly,
    typeof(ZAP.CRM.Catalog.API.Controllers.ProductsController).Assembly,
    typeof(ZAP.Ecosystem.CRM.API.Controllers.CrmPingController).Assembly,
    typeof(ZAP.Ecosystem.Application.CRM.CrmApplicationReference).Assembly,   // CRM handlers
    typeof(ZAP.Ecosystem.Infrastructure.Data.EcosystemDbContext).Assembly,     // CRM infrastructure
    // Sales Module
    typeof(ZAP.Ecosystem.Sales.API.Controllers.SalesPingController).Assembly,
    typeof(ZAP.Ecosystem.Sales.Application.ModuleReference).Assembly,
    typeof(ZAP.Ecosystem.Sales.Infrastructure.ModuleReference).Assembly,
    typeof(ZAP.Ecosystem.Sales.Domain.ModuleReference).Assembly,
    // Inventory Module
    typeof(ZAP.Ecosystem.Inventory.API.Controllers.InventoryPingController).Assembly,
    typeof(ZAP.Ecosystem.Inventory.Application.ModuleReference).Assembly,
    typeof(ZAP.Ecosystem.Inventory.Infrastructure.ModuleReference).Assembly,
    typeof(ZAP.Ecosystem.Inventory.Domain.ModuleReference).Assembly,
    // HRM Module
    typeof(ZAP.Ecosystem.HRM.API.Controllers.HRMPingController).Assembly,
    typeof(ZAP.Ecosystem.HRM.Application.ModuleReference).Assembly,
    typeof(ZAP.Ecosystem.HRM.Infrastructure.ModuleReference).Assembly,
    typeof(ZAP.Ecosystem.HRM.Domain.ModuleReference).Assembly,
    // Finance Module
    typeof(ZAP.Ecosystem.Finance.API.Controllers.FinancePingController).Assembly,
    typeof(ZAP.Ecosystem.Finance.Application.ModuleReference).Assembly,
    typeof(ZAP.Ecosystem.Finance.Infrastructure.ModuleReference).Assembly,
    typeof(ZAP.Ecosystem.Finance.Domain.ModuleReference).Assembly
};

// Register MediatR & Behaviors
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(assemblies);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>)); 
});

// Register Repositories dynamically
foreach (var assembly in assemblies)
{
    var repositoryTypes = assembly.GetTypes()
        .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"));

    foreach (var implementationType in repositoryTypes)
    {
        var interfaceType = implementationType.GetInterfaces()
            .FirstOrDefault(i => i.Name == $"I{implementationType.Name}");

        if (interfaceType != null)
        {
            builder.Services.AddScoped(interfaceType, implementationType);
        }
    }
}

// Scan for validators
builder.Services.AddValidatorsFromAssemblies(assemblies);

var app = builder.Build();

// --- 6. Middleware Pipeline ---
app.MapOpenApi();
app.MapHealthChecks("/health");

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Default redirect to UI
app.MapGet("/", (HttpContext ctx) => 
{
    ctx.Response.Redirect("index.html");
    return Task.CompletedTask;
});

app.UseAuthentication();
app.UseAuthorization();
app.UseSharedAcceptLanguage();

app.MapControllers();
app.Run();

// Development Mocks - REMOVED FOR PRODUCTION DATA FLOW


