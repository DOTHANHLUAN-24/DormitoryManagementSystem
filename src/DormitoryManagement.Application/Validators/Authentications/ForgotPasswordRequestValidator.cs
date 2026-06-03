using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.Authentications;

namespace DormitoryManagement.Application.Validators.Authentications
{
    public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
    {
        public ForgotPasswordRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Vui lòng nhập Email")
                .EmailAddress().WithMessage("Email không hợp lệ");
        }
    }
}
