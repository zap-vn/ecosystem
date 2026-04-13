using System.Text.Json.Serialization;
using MediatR;

namespace ZAP.Identity.Application.Features.Auth.AppAuth.V1.Queries;

public class LoginResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;
    
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;
}

public class LoginCustomerQuery : IRequest<LoginResponse>
{
    [JsonPropertyName("dialing_code")]
    public string DialingCode { get; set; } = string.Empty;

    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;
}
