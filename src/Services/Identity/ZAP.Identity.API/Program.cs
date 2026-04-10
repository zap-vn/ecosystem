using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZAP.Identity.Application.Features.Auth.AppAuth.v1.Commands.LoginUser;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Đăng ký MediatR cho API và Application assemblies
builder.Services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssembly(typeof(LoginUserCommand).Assembly);
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Đăng ký giả lập cho các Interfaces (Do chưa nối vào cơ sở dữ liệu thật)
builder.Services.AddSingleton<ZAP.Identity.Application.Common.Interfaces.IUserRepository, MockUserRepository>();
builder.Services.AddSingleton<ZAP.Identity.Application.Common.Interfaces.ITokenGenerator, MockTokenGenerator>();
builder.Services.AddSingleton<ZAP.Identity.Application.Common.Interfaces.IOtpRepository, MockOtpRepository>();
builder.Services.AddSingleton<ZAP.Identity.Application.Common.Interfaces.INotificationService, MockNotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Nếu muốn có Swagger UI thì cần cài package Microsoft.AspNetCore.OpenApi
}

app.UseHttpsRedirection();
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
