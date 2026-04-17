using Newtonsoft.Json;
using MediatR;

namespace ZAP.Identity.Application.Features.Auth.AppAuth.V1.Queries;

public class LoginResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = string.Empty;
    
    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;
}

public class LoginCustomerQuery : IRequest<LoginResponse>
{
    [JsonProperty("dialing_code")]
    public string DialingCode { get; set; } = string.Empty;

    [JsonProperty("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    [JsonProperty("password")]
    public string Password { get; set; } = string.Empty;
}


