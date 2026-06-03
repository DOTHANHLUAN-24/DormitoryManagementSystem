using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.Users;

namespace DormitoryManagement.Application.Validators.Users
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ tên không được để trống");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email không được để trống")
                .EmailAddress().WithMessage("Email không đúng định dạng");

            RuleFor(x => x.IdentityCardNumber)
                .NotEmpty().WithMessage("Số CCCD không được để trống")
                .Length(9, 12).WithMessage("CCCD không hợp lệ");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Mã số không được để trống");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Vui lòng chọn vai trò");

            RuleFor(x => x.NewPassword)
                .MinimumLength(6).WithMessage("Mật khẩu phải dài ít nhất 6 ký tự.")
                .When(x => !string.IsNullOrEmpty(x.NewPassword));

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword).WithMessage("Mật khẩu xác nhận không khớp.")
                .When(x => !string.IsNullOrEmpty(x.NewPassword));
        }
    }
}
