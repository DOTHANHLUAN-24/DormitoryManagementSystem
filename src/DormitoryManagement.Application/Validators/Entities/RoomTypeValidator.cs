using FluentValidation;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Validators.Entities
{
    public class RoomTypeValidator : AbstractValidator<RoomType>
    {
        public RoomTypeValidator()
        {
            RuleFor(x => x.TypeName)
                .NotEmpty().WithMessage("Tên loại phòng không được để trống")
                .MaximumLength(50).WithMessage("Tên loại phòng không được quá 50 ký tự");

            RuleFor(x => x.BasePrice)
                .GreaterThanOrEqualTo(0).WithMessage("Giá cơ bản phải lớn hơn hoặc bằng 0")
                .LessThanOrEqualTo(1000000000).WithMessage("Giá cơ bản không hợp lệ");

            RuleFor(x => x.MaxOccupants)
                .GreaterThan(0).WithMessage("Sức chứa phải lớn hơn 0")
                .LessThanOrEqualTo(20).WithMessage("Sức chứa không được vượt quá 20 người");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không được quá 500 ký tự");
        }
    }
}
