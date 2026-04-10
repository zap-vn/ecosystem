using FluentValidation.TestHelper;
using ZAP.Identity.Application.Features.Auth.AppAuth.V1.Queries;
using Xunit;

namespace ZAP.Identity.UnitTests.Features.Auth.AppAuth.V1.Queries;

public class LoginCustomerQueryValidatorTests
{
    private readonly LoginCustomerQueryValidator _validator;

    public LoginCustomerQueryValidatorTests()
    {
        _validator = new LoginCustomerQueryValidator();
    }

    [Fact]
    public void Validate_EmptyUsername_ShouldHaveValidationError()
    {
        var query = new LoginCustomerQuery { Username = "", Password = "password123" };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }

    [Fact]
    public void Validate_ShortUsername_ShouldHaveValidationError()
    {
        var query = new LoginCustomerQuery { Username = "ab", Password = "password123" };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Username);
    }

    [Fact]
    public void Validate_EmptyPassword_ShouldHaveValidationError()
    {
        var query = new LoginCustomerQuery { Username = "validUser", Password = "" };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Validate_ShortPassword_ShouldHaveValidationError()
    {
        var query = new LoginCustomerQuery { Username = "validUser", Password = "123" };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Validate_ValidQuery_ShouldNotHaveAnyErrors()
    {
        var query = new LoginCustomerQuery { Username = "validUser", Password = "password123" };
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
