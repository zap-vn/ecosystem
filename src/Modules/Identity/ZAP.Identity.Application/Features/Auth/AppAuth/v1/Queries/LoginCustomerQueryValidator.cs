using FluentValidation;

namespace ZAP.Identity.Application.Features.Auth.AppAuth.V1.Queries;

public class LoginCustomerQueryValidator : AbstractValidator<LoginCustomerQuery>
{
    public LoginCustomerQueryValidator()
    {
        RuleFor(x => x.DialingCode)
            .NotEmpty().WithMessage("Dialing code is required");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required")
            .MinimumLength(8).WithMessage("Phone number must be at least 8 characters")
            .Matches(@"^\d+$").WithMessage("Phone number must contain only digits");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
}


