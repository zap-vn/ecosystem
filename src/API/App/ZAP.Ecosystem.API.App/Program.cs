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
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    .AddApplicationPart(typeof(ZAP.Ecosystem.CRM.API.Controllers.CustomersController).Assembly);

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
        IssuerSigningKey = new SymmetricSecurityKey(System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))),
        ValidAlgorithms = new[] { Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256 }
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

// --- Core & Shared Services ---
builder.Services.AddScoped<ZAP.Ecosystem.Shared.Interfaces.ICurrentUserService, HttpContextCurrentUserService>();

builder.Services.AddDbContext<EcosystemDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem"));
});

builder.Services.AddScoped(typeof(ZAP.Ecosystem.Shared.Data.IBaseRepository<>), typeof(ZAP.Ecosystem.Shared.Data.BaseRepository<>));
builder.Services.AddScoped<Microsoft.EntityFrameworkCore.DbContext>(provider => provider.GetRequiredService<EcosystemDbContext>());

// --- Modular Assembly Scanning ---
var assemblies = new[]
{
    typeof(ZAP.Ecosystem.CRM.Domain.Entities.Customers.CustomerEntity).Assembly,
    typeof(ZAP.Ecosystem.Infrastructure.Data.EcosystemDbContext).Assembly,
    typeof(ZAP.CRM.Catalog.Domain.Entities.Brands.Brand).Assembly,
    typeof(ZAP.CRM.Catalog.Application.DependencyInjection).Assembly,
    typeof(ZAP.CRM.Catalog.Infrastructure.Repositories.Products.ProductRepository).Assembly
};

// Register MediatR
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblies(assemblies);
    cfg.RegisterServicesFromAssembly(typeof(ZAP.Ecosystem.Application.App.Features.Customers.Profile.v1.Queries.GetCustomerProfileQuery).Assembly);
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

// ===================== IMPLEMENTATIONS =====================
public class HttpContextCurrentUserService : ZAP.Ecosystem.Shared.Interfaces.ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HttpContextCurrentUserService(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
    public string? TenantId => _httpContextAccessor.HttpContext?.User?.FindFirst("tenant_id")?.Value;
    public string? UserGuid => _httpContextAccessor.HttpContext?.User?.FindFirst("user_id")?.Value;

    public int LocaleId
    {
        get
        {
            var raw = _httpContextAccessor.HttpContext?.User?.FindFirst("LocaleId")?.Value;
            return int.TryParse(raw, out var id) ? id : 2; // default: Vietnamese
        }
    }
}


