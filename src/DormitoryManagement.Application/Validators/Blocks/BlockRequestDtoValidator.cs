using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.Blocks;

namespace DormitoryManagement.Application.Validators.Blocks
{
    public class BlockRequestDtoValidator : AbstractValidator<BlockRequestDto>
    {
        public BlockRequestDtoValidator()
        {
            RuleFor(x => x.BlockName)
                .NotEmpty().WithMessage("Tên tòa nhà không được để trống")
                .MaximumLength(100).WithMessage("Tên tòa nhà không được vượt quá 100 ký tự");

            RuleFor(x => x.TotalFloors)
                .InclusiveBetween(1, 100).WithMessage("Số tầng phải từ 1 đến 100");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được vượt quá 500 ký tự");
        }
    }
}
