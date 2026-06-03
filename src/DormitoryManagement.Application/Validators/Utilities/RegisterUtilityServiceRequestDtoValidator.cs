using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.Utilities;

namespace DormitoryManagement.Application.Validators.Utilities
{
    public class RegisterUtilityServiceRequestDtoValidator : AbstractValidator<RegisterUtilityServiceRequestDto>
    {
        public RegisterUtilityServiceRequestDtoValidator()
        {
            RuleFor(x => x.UtilityId)
                .NotEmpty().WithMessage("Vui lòng chọn dịch vụ tiện ích.");

            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, 100).WithMessage("Số lượng phải từ 1 đến 100.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Ghi chú không được vượt quá 500 ký tự.");
        }
    }
}
