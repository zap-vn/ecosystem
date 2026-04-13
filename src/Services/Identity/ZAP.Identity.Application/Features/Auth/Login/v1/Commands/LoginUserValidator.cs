using FluentValidation;

namespace ZAP.Identity.Application.Features.Auth.Login.v1.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Account)
            .NotEmpty().WithMessage("error_missing_account|Vui lòng nhập Email hoặc Số điện thoại.");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("error_missing_password|Vui lòng nhập mật khẩu.");
    }
}
