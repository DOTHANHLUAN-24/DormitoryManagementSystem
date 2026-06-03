using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.Assets;

namespace DormitoryManagement.Application.Validators.Assets
{
    public class CreateAssetRequestValidator : AbstractValidator<CreateAssetRequest>
    {
        public CreateAssetRequestValidator()
        {
            RuleFor(x => x.AssetName)
                .NotEmpty().WithMessage("Tên tài sản không được để trống")
                .MaximumLength(100).WithMessage("Tên tài sản không được quá 100 ký tự");

            RuleFor(x => x.AssetCode)
                .NotEmpty().WithMessage("Mã tài sản không được để trống")
                .MaximumLength(50).WithMessage("Mã tài sản không được quá 50 ký tự");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được quá 500 ký tự");

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("Phòng không được để trống");

            RuleFor(x => x.ReplacementCost)
                .GreaterThanOrEqualTo(0).WithMessage("Giá trị đền bù không hợp lệ");
        }
    }
}
