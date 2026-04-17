using FluentValidation;

namespace ZAP.Identity.Application.Features.Auth.Login.v1.Commands.CheckAccountAvailability;

public class CheckAccountAvailabilityCommandValidator : AbstractValidator<CheckAccountAvailabilityCommand>
{
    public CheckAccountAvailabilityCommandValidator()
    {
        RuleFor(x => x.Account)
            .NotEmpty().WithMessage("error_missing_account|Vui lòng nhập Email hoặc Số điện thoại.")
            .MinimumLength(3).WithMessage("error_invalid_account|Tài khoản không hợp lệ.");
    }
}


