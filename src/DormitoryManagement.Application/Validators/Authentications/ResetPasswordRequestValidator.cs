using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.Authentications;

namespace DormitoryManagement.Application.Validators.Authentications
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty().WithMessage("Token xác thực không được để trống");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Vui lòng nhập Email")
                .EmailAddress().WithMessage("Email không hợp lệ");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Vui lòng nhập mật khẩu mới")
                .MinimumLength(6).WithMessage("Mật khẩu ít nhất 6 ký tự");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Mật khẩu xác nhận không khớp");
        }
    }
}
