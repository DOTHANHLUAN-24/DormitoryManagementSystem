using FluentValidation;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Validators.Entities
{
    public class UtilityValidator : AbstractValidator<Utility>
    {
        public UtilityValidator()
        {
            RuleFor(x => x.UtilityName)
                .NotEmpty().WithMessage("Tên dịch vụ không được để trống")
                .MaximumLength(100).WithMessage("Tên dịch vụ không được quá 100 ký tự");

            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Đơn giá phải lớn hơn hoặc bằng 0");

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Đơn vị tính không được để trống")
                .MaximumLength(50).WithMessage("Đơn vị tính không được quá 50 ký tự");
        }
    }
}
