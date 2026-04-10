using MediatR;

namespace ZAP.Identity.Application.Features.Auth.AppAuth.V1.Queries;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}

public class LoginCustomerQuery : IRequest<LoginResponse>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
