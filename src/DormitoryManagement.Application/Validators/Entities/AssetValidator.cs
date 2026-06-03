using FluentValidation;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Validators.Entities
{
    public class AssetValidator : AbstractValidator<Asset>
    {
        public AssetValidator()
        {
            RuleFor(x => x.AssetName)
                .NotEmpty().WithMessage("Tên tài sản không được để trống")
                .MaximumLength(100).WithMessage("Tên tài sản không được quá 100 ký tự");

            RuleFor(x => x.AssetCode)
                .MaximumLength(50).WithMessage("Mã tài sản không được quá 50 ký tự");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được quá 500 ký tự")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("Phòng không được để trống");

            RuleFor(x => x.ReplacementCost)
                .GreaterThanOrEqualTo(0).WithMessage("Giá trị đền bù không hợp lệ");
        }
    }
}
