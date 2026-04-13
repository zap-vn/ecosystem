using FluentValidation;

namespace ZAP.Identity.Application.Features.Auth.Login.v1.Commands.SendOtp;

public class SendOtpCommandValidator : AbstractValidator<SendOtpCommand>
{
    public SendOtpCommandValidator()
    {
        RuleFor(x => x.Account)
            .NotEmpty().WithMessage("error_missing_account|Vui lòng nhập Email hoặc Số điện thoại.");
    }
}
