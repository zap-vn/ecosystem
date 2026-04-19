using Moq;
using Shouldly;
using ZAP.CRM.Catalog.Application.Features.Commons.Units;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ZAP.CRM.Catalog.UnitTests.Features.Commons.Units;

public class CreateUnitCommandHandlerTests
{
    private readonly Mock<IUnitRepository> _mockRepository;
    private readonly CreateUnitCommandHandler _handler;

    public CreateUnitCommandHandlerTests()
    {
        _mockRepository = new Mock<IUnitRepository>();
        _handler = new CreateUnitCommandHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldGenerateNextIdAndSave()
    {
        // Arrange
        var command = new CreateUnitCommand { code = "BOX", name = "Thùng", precision = 0, is_active = true };
        _mockRepository.Setup(x => x.GetMaxIdAsync()).ReturnsAsync(10); // Current max ID is 10

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        // Verify next ID was assigned (10 + 1 = 11)
        _mockRepository.Verify(x => x.AddAsync(It.Is<UomItem>(u => u.id == 11), It.IsAny<CancellationToken>()), Times.Once);
        _mockRepository.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

        // Verify response
        result.ShouldNotBeNull();
        var json = System.Text.Json.JsonSerializer.Serialize(result);
        var doc = System.Text.Json.JsonDocument.Parse(json);
        doc.RootElement.GetProperty("data").GetProperty("id").GetInt32().ShouldBe(11);
    }
}
