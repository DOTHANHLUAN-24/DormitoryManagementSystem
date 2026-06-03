using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests;

namespace DormitoryManagement.Application.Validators.Authentications
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Tên đăng nhập không được để trống");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Mật khẩu không được để trống");
        }
    }
}
