using FluentValidation;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Validators.Entities
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Tên đăng nhập không được để trống")
                .MaximumLength(256).WithMessage("Tên đăng nhập không được quá 256 ký tự");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không đúng định dạng");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ tên không được để trống")
                .MaximumLength(100).WithMessage("Họ tên không được quá 100 ký tự");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Mã số (MSSV/NV) không được để trống")
                .MaximumLength(50).WithMessage("Mã số không được quá 50 ký tự");

            RuleFor(x => x.IdentityCardNumber)
                .NotEmpty().WithMessage("Số CCCD không được để trống")
                .Length(9, 12).WithMessage("Số CCCD phải từ 9 đến 12 ký tự");

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20).WithMessage("Số điện thoại không được quá 20 ký tự")
                .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
        }
    }
}
