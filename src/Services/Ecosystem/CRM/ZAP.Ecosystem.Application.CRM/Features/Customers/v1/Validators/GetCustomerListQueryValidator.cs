using FluentValidation;
using ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Queries;

namespace ZAP.Ecosystem.Application.CRM.Features.Customers.v1.Validators
{
    public class GetCustomerListQueryValidator : AbstractValidator<GetCustomerListQuery>
    {
        public GetCustomerListQueryValidator()
        {
            RuleFor(x => x.Request.PageIndex)
                .GreaterThan(0).WithMessage("Page index must be greater than 0");

            RuleFor(x => x.Request.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0")
                .LessThanOrEqualTo(100).WithMessage("Page size must be less than or equal to 100");
        }
    }
}
