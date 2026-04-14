using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using ZAP.Ecosystem.Shared.Interfaces;

namespace ZAP.Ecosystem.API.CRM.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public string? TenantId => _httpContextAccessor.HttpContext?.User?.FindFirstValue("tenant_id");
        public string? UserGuid => _httpContextAccessor.HttpContext?.User?.FindFirstValue("user_id") ?? "a6b32eee-a14a-4cec-a070-e23b6ea234fb"; // fallback for testing without token setup
        public int LocaleId => _httpContextAccessor.HttpContext?.Items["LocaleId"] is int loc ? loc : 2;
    }
}
