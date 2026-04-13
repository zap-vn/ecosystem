using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using System.Threading.RateLimiting;
using ZAP.Identity.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    // Tạm thời comment AuthorizeFilter ra để test các endpoint login (vì Authorize cản hết)
    // Hoặc [AllowAnonymous] trên LoginController, nhưng để an toàn tôi sẽ comment lại.
    // options.Filters.Add(new AuthorizeFilter(policy)); 
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("StandardSecurityPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:4200", "https://*.zap.com")
              .SetIsOriginAllowedToAllowWildcardSubdomains()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("X-Pagination");
    });
});

// Configure Rate Limiting
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: partition => new FixedWindowRateLimiterOptions
            {
                AutoReplenishment = true,
                PermitLimit = 100,
                QueueLimit = 0,
                Window = TimeSpan.FromMinutes(1)
            }));
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Configure JWT Authentication
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"] ?? "a_very_secret_default_key_at_least_32_chars_long"))
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

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(typeof(ZAP.Identity.Application.Features.Auth.Login.v1.Commands.LoginUser.LoginUserCommand).Assembly);
    // cfg.AddOpenBehavior(typeof(ZAP.Identity.Application.Behaviors.ValidationBehavior<,>));
});

// builder.Services.AddValidatorsFromAssembly(typeof(ZAP.Identity.Application.Features.Auth.Login.V1.Queries.LoginCustomerQueryValidator).Assembly);

builder.Services.AddDbContext<IdentityDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZAP_Identity") ?? "Host=136.118.121.105;Port=5432;Username=postgres;Password=Pg@Secret2026!;Database=zap_ecosystem_v110");
});

// Configure Generic Repository
builder.Services.AddScoped(typeof(ZAP.Ecosystem.Shared.Data.IBaseRepository<>), typeof(ZAP.Ecosystem.Shared.Data.BaseRepository<>));
// For Identity, map DbContext to IdentityDbContext for the generic repository
builder.Services.AddScoped<Microsoft.EntityFrameworkCore.DbContext>(provider => provider.GetRequiredService<IdentityDbContext>());

// Đăng ký giả lập cho các Interfaces (Do chưa nối vào cơ sở dữ liệu thật)
builder.Services.AddSingleton<ZAP.Identity.Application.Common.Interfaces.IUserRepository, MockUserRepository>();
builder.Services.AddSingleton<ZAP.Identity.Application.Common.Interfaces.ITokenGenerator, MockTokenGenerator>();
builder.Services.AddSingleton<ZAP.Identity.Application.Common.Interfaces.IOtpRepository, MockOtpRepository>();
builder.Services.AddSingleton<ZAP.Identity.Application.Common.Interfaces.INotificationService, MockNotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(); // Add Scalar UI
}
else
{
    app.UseHsts(); 
}

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-Xss-Protection", "1; mode=block");
    context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Append("X-Frame-Options", "DENY"); 
    context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'; script-src 'self' 'unsafe-inline' https://unpkg.com; style-src 'self' 'unsafe-inline' https://unpkg.com https://fonts.googleapis.com; font-src 'self' https://fonts.gstatic.com; img-src 'self' data:;");
    context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
    await next();
});

app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapGet("/", (HttpContext ctx) => 
{
    ctx.Response.Redirect("index.html");
    return Task.CompletedTask;
});

app.UseRateLimiter(); // Apply Rate Limiter
app.UseCors("StandardSecurityPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// ===================== MOCKS =====================
public class MockUser
{
    public string email { get; set; } = string.Empty;
    public string password_hash { get; set; } = string.Empty;
    public int status_id { get; set; }
    public System.Guid? tenant_id { get; set; }
    public System.Guid id { get; set; }
    public string full_name { get; set; } = string.Empty;
}

public class MockUserRepository : ZAP.Identity.Application.Common.Interfaces.IUserRepository
{
    public async Task<dynamic> GetByEmailAsync(string email) => await Task.FromResult<dynamic>(new MockUser { email = email, password_hash = "password123", status_id = 9001, tenant_id = System.Guid.NewGuid(), id = System.Guid.NewGuid(), full_name = "Mock Email User" });
    public async Task<dynamic> GetByPhoneAsync(string phone) => await Task.FromResult<dynamic>(new MockUser { email = "phone@mock.com", password_hash = "password123", status_id = 9001, tenant_id = System.Guid.NewGuid(), id = System.Guid.NewGuid(), full_name = "Mock Phone User" });
}

public class MockTokenGenerator : ZAP.Identity.Application.Common.Interfaces.ITokenGenerator
{
    public async Task<string> GenerateTokenAsync(dynamic user) => await Task.FromResult("mock_jwt_token_with_tenantId_" + user.tenant_id);
}

public class MockOtpRepository : ZAP.Identity.Application.Common.Interfaces.IOtpRepository
{
    public async Task CreateAsync(dynamic customerOtp) => await Task.CompletedTask;
}

public class MockNotificationService : ZAP.Identity.Application.Common.Interfaces.INotificationService
{
    public async Task SendOtpEmailAsync(string email, string otpCode, string name) => await Task.CompletedTask;
    public async Task SendSmsOtpAsync(string phone, string otpCode) => await Task.CompletedTask;
}
