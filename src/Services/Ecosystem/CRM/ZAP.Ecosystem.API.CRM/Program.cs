using Microsoft.EntityFrameworkCore;
using ZAP.Ecosystem.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Asp.Versioning;
using ZAP.Ecosystem.Shared.Middlewares;
using Scalar.AspNetCore;
using ZAP.Ecosystem.Application.CRM.Behaviors;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);

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

// FluentValidation + pipeline
builder.Services.AddValidatorsFromAssembly(typeof(ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Validators.GetCustomerListQueryValidator).Assembly);
builder.Services.AddTransient(typeof(MediatR.IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// MediatR scanning for CRM features
builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(ZAP.Ecosystem.Application.CRM.Features.Categories.v1.Queries.GetCategoryListQuery).Assembly);
});

builder.Services.AddDbContext<EcosystemDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Ecosystem"));
});

// Configure Generic Repository
builder.Services.AddScoped(typeof(ZAP.Ecosystem.Shared.Data.IBaseRepository<>), typeof(ZAP.Ecosystem.Shared.Data.BaseRepository<>));
builder.Services.AddScoped<Microsoft.EntityFrameworkCore.DbContext>(provider => provider.GetRequiredService<EcosystemDbContext>());

// Auto-register repositories from Infrastructure
var infrastructureAssembly = typeof(ZAP.Ecosystem.Infrastructure.Data.Repositories.CRM.ProductRepository).Assembly;
var repositoryTypes = infrastructureAssembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository") && !t.IsGenericType);

foreach (var repoType in repositoryTypes)
{
    var interfaceType = repoType.GetInterfaces().FirstOrDefault(i => i.Name == "I" + repoType.Name);
    if (interfaceType != null)
    {
        builder.Services.AddScoped(interfaceType, repoType);
    }
}

var app = builder.Build();

app.MapOpenApi();
app.MapHealthChecks("/health");

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
app.UseSharedAcceptLanguage();

app.MapControllers();

app.Run();
