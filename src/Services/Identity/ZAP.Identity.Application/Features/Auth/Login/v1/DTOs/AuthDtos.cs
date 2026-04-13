using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ZAP.Identity.Application.Features.Auth.Login.v1.DTOs
{
    public class CheckAccountResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public CheckAccountDataDto? Data { get; set; }
    }

    public class CheckAccountDataDto
    {
        public bool Exists { get; set; }
        public List<string> Methods { get; set; } = new();
    }

    public class LoginResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public LoginDataDto? Data { get; set; }
    }

    public class LoginDataDto
    {
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        
        [JsonPropertyName("merchantId")]
        public string MerchantId { get; set; } = string.Empty;
        
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;
        
        [JsonPropertyName("fullname")]
        public string FullName { get; set; } = string.Empty;
        
        [JsonPropertyName("logoUrl")]
        public string LogoUrl { get; set; } = string.Empty;
    }
}
