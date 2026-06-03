using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.Users;

namespace DormitoryManagement.Application.Validators.Users
{
    public class UserRequestDtoValidator : AbstractValidator<UserRequestDto>
    {
        public UserRequestDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Tên đăng nhập là bắt buộc");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Vui lòng nhập mật khẩu")
                .MinimumLength(6).WithMessage("Mật khẩu phải dài ít nhất 6 ký tự.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Vui lòng xác nhận mật khẩu")
                .Equal(x => x.Password).WithMessage("Mật khẩu xác nhận không khớp.");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Họ tên là bắt buộc");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Mã số (MSSV/NV) là bắt buộc");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email không đúng định dạng.")
                .When(x => !string.IsNullOrEmpty(x.Email));

            RuleFor(x => x.IdentityCardNumber)
                .NotEmpty().WithMessage("CCCD là bắt buộc")
                .Length(9, 12).WithMessage("CCCD không hợp lệ");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Vui lòng chọn vai trò");
        }
    }
}
