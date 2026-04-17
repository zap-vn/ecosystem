using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZAP.Ecosystem.Shared.Data;
using ZAP.Identity.Domain.Entities;

namespace ZAP.Identity.Application.Features.Auth.AppAuth.V1.Queries;

public class LoginCustomerQueryHandler : IRequestHandler<LoginCustomerQuery, LoginResponse>
{
    private readonly IBaseRepository<Customer> _customerRepository;
    private readonly IConfiguration _configuration;

    public LoginCustomerQueryHandler(
        IBaseRepository<Customer> customerRepository,
        IConfiguration configuration)
    {
        _customerRepository = customerRepository;
        _configuration = configuration;
    }

    public async Task<LoginResponse> Handle(LoginCustomerQuery request, CancellationToken cancellationToken)
    {
        var result = await _customerRepository.GetPagedAsync(
            pageIndex: 1, 
            pageSize: 1, 
            filter: c => c.DialingCode == request.DialingCode && c.PhoneNumber == request.PhoneNumber && c.IsActive, 
            cancellationToken: cancellationToken);
            
        var customer = result.Items.FirstOrDefault();

        var secretKey = _configuration["JwtSettings:SecretKey"] ?? throw new InvalidOperationException("Missing JWT Secret");
        var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
        var hashedSecretKey = System.Security.Cryptography.SHA256.HashData(secretKeyBytes);

        using var hmac = new System.Security.Cryptography.HMACSHA256(hashedSecretKey);
        var computedPasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)));

        if (customer == null || customer.PasswordHash != computedPasswordHash)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var issuer = _configuration["JwtSettings:Issuer"];
        var audience = _configuration["JwtSettings:Audience"];
        var expiryMinutes = int.TryParse(_configuration["JwtSettings:ExpiryMinutes"], out var minutes) ? minutes : 120;
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{customer.DialingCode}{customer.PhoneNumber}"),
                new Claim("role", "customer_app")
            }),
            Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(hashedSecretKey),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);

        var refreshToken = Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(64));
        customer.RefreshToken = refreshToken;
        customer.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // 7 days refresh validity

        await _customerRepository.UpdateAsync(customer, cancellationToken);
        
        return new LoginResponse 
        { 
            AccessToken = accessToken, 
            RefreshToken = refreshToken 
        };
    }
}


