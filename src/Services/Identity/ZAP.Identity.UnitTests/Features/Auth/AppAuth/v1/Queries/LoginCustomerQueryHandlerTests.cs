using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Linq.Expressions;
using ZAP.Ecosystem.Shared.Data;
using ZAP.Identity.Application.Features.Auth.AppAuth.V1.Queries;
using ZAP.Identity.Domain.Entities;
using Xunit;

namespace ZAP.Identity.UnitTests.Features.Auth.AppAuth.V1.Queries;

public class LoginCustomerQueryHandlerTests
{
    private readonly Mock<IBaseRepository<Customer>> _mockRepository;
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly LoginCustomerQueryHandler _handler;

    public LoginCustomerQueryHandlerTests()
    {
        _mockRepository = new Mock<IBaseRepository<Customer>>();
        _mockConfig = new Mock<IConfiguration>();
        
        // Setup mock configurations required for JWT
        _mockConfig.Setup(c => c["JwtSettings:SecretKey"]).Returns("TestSecretKeyForUnitTestingMustBeLongEnough123!");
        _mockConfig.Setup(c => c["JwtSettings:Issuer"]).Returns("TestIssuer");
        _mockConfig.Setup(c => c["JwtSettings:Audience"]).Returns("TestAudience");
        _mockConfig.Setup(c => c["JwtSettings:ExpiryMinutes"]).Returns("60");

        _handler = new LoginCustomerQueryHandler(_mockRepository.Object, _mockConfig.Object);
    }

    [Fact]
    public async Task Handle_ValidCredentials_ReturnsLoginResponse()
    {
        // Arrange
        var request = new LoginCustomerQuery { Username = "testuser", Password = "password123" };
        var customer = new Customer { Id = Guid.NewGuid(), Username = "testuser", PasswordHash = "password123", IsActive = true };
        
        _mockRepository.Setup(r => r.GetPagedAsync(1, 1, It.IsAny<Expression<Func<Customer, bool>>>(), null, "", It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new PagedResult<Customer> { Items = new List<Customer> { customer } });
        
        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
                       .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().NotBeNullOrEmpty();
        result.RefreshToken.Should().NotBeNullOrEmpty();
        _mockRepository.Verify(r => r.UpdateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidCredentials_ThrowsUnauthorizedException()
    {
         // Arrange
        var request = new LoginCustomerQuery { Username = "testuser", Password = "wrongpassword" };
        var customer = new Customer { Id = Guid.NewGuid(), Username = "testuser", PasswordHash = "password123", IsActive = true };
        
        _mockRepository.Setup(r => r.GetPagedAsync(1, 1, It.IsAny<Expression<Func<Customer, bool>>>(), null, "", It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new PagedResult<Customer> { Items = new List<Customer> { customer } });

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(request, CancellationToken.None));
    }
}
