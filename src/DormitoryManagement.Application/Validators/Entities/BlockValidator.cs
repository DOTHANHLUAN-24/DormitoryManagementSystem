using FluentValidation;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Validators.Entities
{
    public class BlockValidator : AbstractValidator<Block>
    {
        public BlockValidator()
        {
            RuleFor(x => x.BlockName)
                .NotEmpty().WithMessage("Tên tòa nhà không được để trống")
                .MaximumLength(100).WithMessage("Tên tòa nhà không được quá 100 ký tự");

            RuleFor(x => x.TotalFloors)
                .GreaterThan(0).WithMessage("Số tầng phải lớn hơn 0")
                .LessThanOrEqualTo(100).WithMessage("Số tầng không được vượt quá 100");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được quá 500 ký tự");
        }
    }
}
