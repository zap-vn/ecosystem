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

public class GetUnitByIdQueryHandlerTests
{
    private readonly Mock<IUnitRepository> _mockRepository;
    private readonly GetUnitByIdQueryHandler _handler;

    public GetUnitByIdQueryHandlerTests()
    {
        _mockRepository = new Mock<IUnitRepository>();
        _handler = new GetUnitByIdQueryHandler(_mockRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnUnit_WhenExists()
    {
        // Arrange
        var unit = new UomItem { id = 10, code = "KG", name = "Kilogram", is_active = true };
        _mockRepository.Setup(x => x.GetByIdAsync(10, It.IsAny<CancellationToken>())).ReturnsAsync(unit);

        // Act
        var result = await _handler.Handle(new GetUnitByIdQuery(10), CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        var json = System.Text.Json.JsonSerializer.Serialize(result);
        var doc = System.Text.Json.JsonDocument.Parse(json);
        var data = doc.RootElement.GetProperty("data");
        
        data.GetProperty("id").GetInt32().ShouldBe(10);
        data.GetProperty("status").GetProperty("name").GetString().ShouldBe("Active");
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenDoesNotExist()
    {
        // Arrange
        _mockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync((UomItem?)null);

        // Act
        var result = await _handler.Handle(new GetUnitByIdQuery(999), CancellationToken.None);

        // Assert
        var json = System.Text.Json.JsonSerializer.Serialize(result);
        var doc = System.Text.Json.JsonDocument.Parse(json);
        doc.RootElement.GetProperty("code").GetInt32().ShouldBe(404);
    }
}
