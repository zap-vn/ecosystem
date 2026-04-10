using FluentValidation.TestHelper;
using ZAP.Identity.Application.Features.Customers.Profile.V1.Queries;
using Xunit;

namespace ZAP.Identity.UnitTests.Features.Customers.Profile.V1.Queries;

public class GetCustomerProfileQueryValidatorTests
{
    private readonly GetCustomerProfileQueryValidator _validator;

    public GetCustomerProfileQueryValidatorTests()
    {
        _validator = new GetCustomerProfileQueryValidator();
    }

    [Fact]
    public void Validate_EmptyGuid_ShouldHaveValidationError()
    {
        var query = new GetCustomerProfileQuery { CustomerId = Guid.Empty };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }

    [Fact]
    public void Validate_ValidGuid_ShouldNotHaveAnyErrors()
    {
        var query = new GetCustomerProfileQuery { CustomerId = Guid.NewGuid() };
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
