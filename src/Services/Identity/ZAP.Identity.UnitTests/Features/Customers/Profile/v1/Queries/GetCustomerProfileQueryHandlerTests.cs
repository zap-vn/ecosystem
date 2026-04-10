using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using ZAP.Ecosystem.Shared.Data;
using ZAP.Identity.Application.Features.Customers.Profile.V1.Queries;
using ZAP.Identity.Domain.Entities;
using Xunit;

namespace ZAP.Identity.UnitTests.Features.Customers.Profile.V1.Queries;

public class GetCustomerProfileQueryHandlerTests
{
    private readonly Mock<IBaseRepository<Customer>> _mockRepository;
    private readonly GetCustomerProfileQueryHandler _handler;

    public GetCustomerProfileQueryHandlerTests()
    {
        _mockRepository = new Mock<IBaseRepository<Customer>>();
        _handler = new GetCustomerProfileQueryHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_CustomerExists_ReturnsProfileDto()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var request = new GetCustomerProfileQuery { CustomerId = customerId };
        
        var customer = new Customer 
        { 
            Id = customerId, 
            Username = "profileuser", 
            Email = "user@test.com", 
            IsActive = true 
        };

        _mockRepository.Setup(r => r.GetPagedAsync(1, 1, It.IsAny<Expression<Func<Customer, bool>>>(), null, "", It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new PagedResult<Customer> { Items = new List<Customer> { customer } });

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(customerId);
        result.Email.Should().Be("user@test.com");
        result.SubscriptionLevel.Should().NotBeNullOrEmpty(); // Simulated join data
    }

    [Fact]
    public async Task Handle_CustomerNotFound_ThrowsException()
    {
        // Arrange
        var request = new GetCustomerProfileQuery { CustomerId = Guid.NewGuid() };
        
        _mockRepository.Setup(r => r.GetPagedAsync(1, 1, It.IsAny<Expression<Func<Customer, bool>>>(), null, "", It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new PagedResult<Customer> { Items = new List<Customer>() });

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));
    }
}
