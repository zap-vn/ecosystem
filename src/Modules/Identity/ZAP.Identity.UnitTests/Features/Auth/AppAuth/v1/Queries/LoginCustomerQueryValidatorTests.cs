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
    public void Validate_EmptyDialingCode_ShouldHaveValidationError()
    {
        var query = new LoginCustomerQuery { DialingCode = "", PhoneNumber = "388888888", Password = "password123" };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.DialingCode);
    }

    [Theory]
    [InlineData("388888888")]
    [InlineData("0988888888")]
    [InlineData("12345678")]
    [InlineData("01234567890123")] // Long phone numbers
    public void Validate_ValidPhoneNumberFormats_ShouldNotHaveAnyErrors(string phoneNumber)
    {
        var query = new LoginCustomerQuery { DialingCode = "+84", PhoneNumber = phoneNumber, Password = "password123" };
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber);
    }

    [Theory]
    [InlineData(null)]          // Null
    [InlineData("")]            // Empty
    [InlineData("   ")]         // Whitespace
    [InlineData("1234567")]     // Too short (7 digits)
    [InlineData("abcdefgh")]    // Letters only
    [InlineData("1234 5678")]   // Contains space
    [InlineData("1234-5678")]   // Contains hyphen
    [InlineData("+8438888888")] // Contains plus notation inside phone_number (should be in dialing_code)
    [InlineData("12345678a")]   // Contains letter at the end
    public void Validate_InvalidPhoneNumberFormats_ShouldHaveValidationError(string? phoneNumber)
    {
        var query = new LoginCustomerQuery { DialingCode = "+84", PhoneNumber = phoneNumber!, Password = "password123" };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
    }

    [Fact]
    public void Validate_EmptyPassword_ShouldHaveValidationError()
    {
        var query = new LoginCustomerQuery { DialingCode = "+84", PhoneNumber = "388888888", Password = "" };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Validate_ShortPassword_ShouldHaveValidationError()
    {
        var query = new LoginCustomerQuery { DialingCode = "+84", PhoneNumber = "388888888", Password = "123" };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void Validate_ValidQuery_ShouldNotHaveAnyErrors()
    {
        var query = new LoginCustomerQuery { DialingCode = "+84", PhoneNumber = "388888888", Password = "password123" };
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }
}


