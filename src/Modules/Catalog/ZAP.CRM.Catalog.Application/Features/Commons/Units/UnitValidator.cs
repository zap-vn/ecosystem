using FluentValidation;

namespace ZAP.CRM.Catalog.Application.Features.Commons.Units;

public class CreateUnitCommandValidator : AbstractValidator<CreateUnitCommand>
{
    public CreateUnitCommandValidator()
    {
        RuleFor(x => x.code).NotEmpty().MaximumLength(50);
        RuleFor(x => x.name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.precision).InclusiveBetween(0, 5);
    }
}

public class UpdateUnitCommandValidator : AbstractValidator<UpdateUnitCommand>
{
    public UpdateUnitCommandValidator()
    {
        RuleFor(x => x.id).NotEmpty();
        RuleFor(x => x.code).NotEmpty().MaximumLength(50);
        RuleFor(x => x.name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.precision).InclusiveBetween(0, 5);
    }
}
