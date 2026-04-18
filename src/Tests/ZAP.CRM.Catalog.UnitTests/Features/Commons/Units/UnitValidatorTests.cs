using Shouldly;
using ZAP.CRM.Catalog.Application.Features.Commons.Units;
using Xunit;

namespace ZAP.CRM.Catalog.UnitTests.Features.Commons.Units;

public class UnitValidatorTests
{
    private readonly CreateUnitCommandValidator _createValidator;
    private readonly UpdateUnitCommandValidator _updateValidator;

    public UnitValidatorTests()
    {
        _createValidator = new CreateUnitCommandValidator();
        _updateValidator = new UpdateUnitCommandValidator();
    }

    [Theory]
    [InlineData("", "Name", 0, "code")] // Code empty
    [InlineData("CODE", "", 0, "name")] // Name empty
    [InlineData("CODE", "Name", -1, "precision")] // Precision too low
    [InlineData("CODE", "Name", 6, "precision")] // Precision too high
    public void CreateValidator_ShouldHaveError_WhenInvalid(string code, string name, int precision, string errorField)
    {
        // Arrange
        var command = new CreateUnitCommand { code = code, name = name, precision = precision };

        // Act
        var result = _createValidator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(x => x.PropertyName == errorField);
    }

    [Fact]
    public void CreateValidator_ShouldBeValid_WhenDataIsCorrect()
    {
        // Arrange
        var command = new CreateUnitCommand { code = "KG", name = "Kilogram", precision = 2 };

        // Act
        var result = _createValidator.Validate(command);

        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void UpdateValidator_ShouldHaveError_WhenIdIsZero()
    {
        // Arrange
        var command = new UpdateUnitCommand { id = 0, code = "KG", name = "KG", precision = 0 };

        // Act
        var result = _updateValidator.Validate(command);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(x => x.PropertyName == "id");
    }
}
