using Moq;
using Shouldly;
using ZAP.CRM.Catalog.Application.Features.Commons.Units;
using ZAP.CRM.Catalog.Domain.Entities.Commons;
using ZAP.CRM.Catalog.Domain.Interfaces.Commons;
using ZAP.Ecosystem.Shared.Interfaces;
using ZAP.Ecosystem.Shared.Data;
using Xunit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace ZAP.CRM.Catalog.UnitTests.Features.Commons.Units;

public class GetUnitsListQueryHandlerTests
{
    private readonly Mock<IUnitRepository> _mockRepository;
    private readonly Mock<ICurrentUserService> _mockUserService;
    private readonly GetUnitsListQueryHandler _handler;

    public GetUnitsListQueryHandlerTests()
    {
        _mockRepository = new Mock<IUnitRepository>();
        _mockUserService = new Mock<ICurrentUserService>();
        _handler = new GetUnitsListQueryHandler(_mockRepository.Object, _mockUserService.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnPagedResponse_WithEnrichedStatus()
    {
        // Arrange
        var request = new GetUnitsListQuery { page_index = 1, page_size = 10 };
        var units = new List<UomItem>
        {
            new UomItem { id = 1, code = "KG", name = "Kilogram", is_active = true, precision = 2 },
            new UomItem { id = 2, code = "G", name = "Gram", is_active = false, precision = 0 }
        };

        _mockRepository.Setup(r => r.GetPagedAsync(
            It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Guid?>(), 
            It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>(), 
            "name", false))
            .ReturnsAsync((units, 2));

        _mockUserService.Setup(u => u.TenantId).Returns(Guid.NewGuid().ToString());

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.ShouldNotBeNull();
        
        // Convert anonymous object to a readable format for testing
        var json = System.Text.Json.JsonSerializer.Serialize(result);
        var doc = System.Text.Json.JsonDocument.Parse(json);
        var root = doc.RootElement;

        root.GetProperty("success").GetBoolean().ShouldBeTrue();
        
        var data = root.GetProperty("data");
        data.GetProperty("total_record").GetInt32().ShouldBe(2);

        var items = data.GetProperty("items");
        items.GetArrayLength().ShouldBe(2);

        // Verify status mapping
        var item1 = items[0];
        item1.GetProperty("status").GetProperty("id").GetInt32().ShouldBe(9001); // Active
        item1.GetProperty("status").GetProperty("name").GetString().ShouldBe("Active");
        item1.GetProperty("status").GetProperty("color").GetString().ShouldBe("#10b981");

        var item2 = items[1];
        item2.GetProperty("status").GetProperty("id").GetInt32().ShouldBe(9002); // Inactive
        item2.GetProperty("status").GetProperty("name").GetString().ShouldBe("Inactive");
        item2.GetProperty("status").GetProperty("color").GetString().ShouldBe("#64748b");
    }
}
