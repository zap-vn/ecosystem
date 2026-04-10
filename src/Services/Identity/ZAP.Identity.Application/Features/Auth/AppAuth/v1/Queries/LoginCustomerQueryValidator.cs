using FluentValidation;

namespace ZAP.Identity.Application.Features.Auth.AppAuth.V1.Queries;

public class LoginCustomerQueryValidator : AbstractValidator<LoginCustomerQuery>
{
    public LoginCustomerQueryValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
}
