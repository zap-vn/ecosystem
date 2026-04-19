using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using ZAP.Identity.Application.Common.Interfaces;
using ZAP.Identity.Application.Features.Auth.Login.v1.Commands.LoginUser;

namespace ZAP.Identity.UnitTests.Features.Auth.Login.v1.Commands;

public class MockUser
{
    public string email { get; set; } = string.Empty;
    public string password_hash { get; set; } = string.Empty;
    public int status_id { get; set; }
    public System.Guid? tenant_id { get; set; }
    public System.Guid id { get; set; }
    public string full_name { get; set; } = string.Empty;
    public System.Guid? avatar_id { get; set; }
    public string? dialing_code { get; set; }
    public string? phone_number { get; set; }
}


public class LoginUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<ITokenGenerator> _mockTokenGenerator;
    private readonly LoginUserCommandHandler _handler;

    public LoginUserCommandHandlerTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockTokenGenerator = new Mock<ITokenGenerator>();
        _handler = new LoginUserCommandHandler(_mockUserRepository.Object, _mockTokenGenerator.Object);
    }

    [Fact]
    public async Task Handle_WithValidCredentials_ReturnsSuccess()
    {
        // Arrange
        var command = new LoginUserCommand { Account = "test@domain.com", Password = "password123" };
        var mockUser = new MockUser {
            email = "test@domain.com",
            password_hash = "password123", // Matches logic
            status_id = 9001,
            tenant_id = System.Guid.NewGuid(),
            id = System.Guid.NewGuid(),
            full_name = "Test User"
        };
        
        _mockUserRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<dynamic>(mockUser));

        _mockTokenGenerator.Setup(x => x.GenerateTokenAsync(It.IsAny<object>()))
            .ReturnsAsync("mocked-jwt");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.Token.Should().Be("mocked-jwt");
    }

    [Fact]
    public async Task Handle_WithInvalidPassword_ReturnsError()
    {
        // Arrange
        var command = new LoginUserCommand { Account = "test@domain.com", Password = "wrongpassword" };
        var mockUser = new MockUser {
            email = "test@domain.com",
            password_hash = "correctpassword123", 
            status_id = 9001,
            tenant_id = System.Guid.NewGuid(),
            id = System.Guid.NewGuid(),
            full_name = "Test User"
        };
        
        _mockUserRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .Returns(Task.FromResult<dynamic>(mockUser));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Message.Should().Contain("AUTH_002");
    }
}
