using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Infrastructure.Data;
<<<<<<< HEAD
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Asp.Versioning;
using ZAP.Ecosystem.Shared.Middlewares;
using Scalar.AspNetCore;
=======
using ZAP.Ecosystem.Application.CRM.Behaviors;
using FluentValidation;
>>>>>>> deploy_crm_dev

var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();

    // Register real Identity Service
    builder.Services.AddScoped<ZAP.Ecosystem.Shared.Interfaces.ICurrentUserService, ZAP.Ecosystem.API.CRM.Services.CurrentUserService>();

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
=======
builder.Services.AddHealthChecks();
builder.Services.AddOpenApi();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
>>>>>>> deploy_crm_dev

builder.Services.AddDbContext<EcosystemDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem"));
});

<<<<<<< HEAD
// Configure Generic Repository
=======
// Generic repository
>>>>>>> deploy_crm_dev
builder.Services.AddScoped(typeof(ZAP.Ecosystem.Shared.Data.IBaseRepository<>), typeof(ZAP.Ecosystem.Shared.Data.BaseRepository<>));
builder.Services.AddScoped<Microsoft.EntityFrameworkCore.DbContext>(provider => provider.GetRequiredService<EcosystemDbContext>());

<<<<<<< HEAD
<<<<<<< HEAD
// Automatically register all EF Core Repositories dynamically
var domainAssembly = typeof(ZAP.Ecosystem.Domain.CRM.ILocationRepository).Assembly;
var infrastructureAssembly = typeof(ZAP.Ecosystem.Infrastructure.Data.EcosystemDbContext).Assembly;

var repositoryInterfaces = domainAssembly.GetTypes()
    .Where(t => t.IsInterface && t.Name.EndsWith("Repository"))
    .ToList();

var repositoryImplementations = infrastructureAssembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"))
    .ToList();

foreach (var ri in repositoryInterfaces)
{
    var impl = repositoryImplementations.FirstOrDefault(t => ri.IsAssignableFrom(t));
    if (impl != null)
    {
        builder.Services.AddScoped(ri, impl);
    }
}
=======
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
>>>>>>> deploy_crm_dev



builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();

    // Register real Identity Service
    builder.Services.AddScoped<ZAP.Ecosystem.Shared.Interfaces.ICurrentUserService, ZAP.Ecosystem.API.CRM.Services.CurrentUserService>();

=======
// FluentValidation + pipeline
builder.Services.AddValidatorsFromAssembly(typeof(ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Validators.GetCustomerListQueryValidator).Assembly);
builder.Services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// MediatR
>>>>>>> deploy_crm_dev
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

<<<<<<< HEAD
app.MapOpenApi();
app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
=======
app.MapHealthChecks("/health");

>>>>>>> deploy_crm_dev
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
<<<<<<< HEAD

app.UseStaticFiles();

app.MapGet("/", (HttpContext ctx) => 
{
    ctx.Response.Redirect("index.html");
    return Task.CompletedTask;
});

app.UseAuthentication();
app.UseAuthorization();
app.UseSharedAcceptLanguage();
=======
>>>>>>> deploy_crm_dev
app.MapControllers();
app.Run();

<<<<<<< HEAD










=======
public class MockCurrentUserService : ZAP.Ecosystem.Application.CRM.Common.Interfaces.ICurrentUserService
{
    public string? UserGuid => "a6b32eee-a14a-4cec-a070-e23b6ea234fb";
    public int LocaleId => 2;
}
>>>>>>> deploy_crm_dev
