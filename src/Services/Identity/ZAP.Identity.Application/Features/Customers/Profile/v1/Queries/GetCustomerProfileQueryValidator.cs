using FluentValidation;

namespace ZAP.Identity.Application.Features.Customers.Profile.V1.Queries;

public class GetCustomerProfileQueryValidator : AbstractValidator<GetCustomerProfileQuery>
{
    public GetCustomerProfileQueryValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required.")
            .NotEqual(Guid.Empty).WithMessage("Customer ID cannot be empty GUID.");
    }
}
