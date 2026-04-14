using FluentValidation;
using ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Queries;

namespace ZAP.Ecosystem.Application.CRM.Features.Locations.v1.Validators
{
    public class GetLocationListQueryValidator : AbstractValidator<GetLocationListQuery>
    {
        public GetLocationListQueryValidator()
        {
            RuleFor(x => x.Request.PageIndex).GreaterThan(0);
            RuleFor(x => x.Request.PageSize).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
