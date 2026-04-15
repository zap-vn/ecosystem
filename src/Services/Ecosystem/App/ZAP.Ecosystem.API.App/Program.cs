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
    .AddApplicationPart(typeof(ZAP.Ecosystem.API.CRM.Features.GeoCountry.v1.Controllers.GeoCountriesController).Assembly);

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

// MediatR scanning for App features
builder.Services.AddMediatR(cfg =>{
    cfg.RegisterServicesFromAssembly(typeof(ZAP.Ecosystem.Application.App.Features.Customers.Profile.v1.Queries.GetCustomerProfileQuery).Assembly);
});

// Register only the GeoCountry MediatR handler explicitly (other CRM handlers registered as needed)
builder.Services.AddTransient<
    MediatR.IRequestHandler<ZAP.Ecosystem.Application.CRM.Features.GeoCountry.v1.Queries.GetGeoCountryListQuery, object>,
    ZAP.Ecosystem.Application.CRM.Features.GeoCountry.v1.Queries.GetGeoCountryListQueryHandler>();

// CRM Repositories
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IGeoCountryRepository,    ZAP.Ecosystem.Infrastructure.Repositories.CRM.GeoCountryRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICategoryRepository,      ZAP.Ecosystem.Infrastructure.Repositories.CRM.CategoryRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICollectionRepository,    ZAP.Ecosystem.Infrastructure.Repositories.CRM.CollectionRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ILocationRepository,      ZAP.Ecosystem.Infrastructure.Repositories.CRM.LocationRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IMenuRepository,          ZAP.Ecosystem.Infrastructure.Repositories.CRM.MenuRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IModifierGroupRepository, ZAP.Ecosystem.Infrastructure.Repositories.CRM.ModifierGroupRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IModifierItemRepository,  ZAP.Ecosystem.Infrastructure.Repositories.CRM.ModifierItemRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IProductRepository,       ZAP.Ecosystem.Infrastructure.Repositories.CRM.ProductRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.ICustomerRepository,      ZAP.Ecosystem.Infrastructure.Repositories.CRM.CustomerRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Domain.CRM.IPromotionRepository,     ZAP.Ecosystem.Infrastructure.Repositories.CRM.PromotionRepository>();
builder.Services.AddScoped<ZAP.Ecosystem.Application.CRM.Common.Interfaces.ICurrentUserService, HttpContextCurrentUserService>();

builder.Services.AddDbContext<EcosystemDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem"));
});

// Configure Generic Repository
builder.Services.AddScoped(typeof(ZAP.Ecosystem.Shared.Data.IBaseRepository<>), typeof(ZAP.Ecosystem.Shared.Data.BaseRepository<>));
builder.Services.AddScoped<Microsoft.EntityFrameworkCore.DbContext>(provider => provider.GetRequiredService<EcosystemDbContext>());

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
public class HttpContextCurrentUserService : ZAP.Ecosystem.Application.CRM.Common.Interfaces.ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public HttpContextCurrentUserService(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    public string? UserGuid
        => _httpContextAccessor.HttpContext?.User?.FindFirst("UserGuid")?.Value
        ?? _httpContextAccessor.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

    public int LocaleId
    {
        get
        {
            var raw = _httpContextAccessor.HttpContext?.User?.FindFirst("LocaleId")?.Value;
            return int.TryParse(raw, out var id) ? id : 2; // default: Vietnamese
        }
    }
}
