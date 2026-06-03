using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.RoomTypes;

namespace DormitoryManagement.Application.Validators.RoomTypes
{
    public class RoomTypeRequestDtoValidator : AbstractValidator<RoomTypeRequestDto>
    {
        public RoomTypeRequestDtoValidator()
        {
            RuleFor(x => x.TypeName)
                .NotEmpty().WithMessage("Tên loại phòng không được để trống")
                .MaximumLength(50).WithMessage("Tên loại phòng không quá 50 ký tự");

            RuleFor(x => x.BasePrice)
                .GreaterThanOrEqualTo(0).WithMessage("Đơn giá phải là số dương");

            RuleFor(x => x.MaxOccupants)
                .InclusiveBetween(1, 20).WithMessage("Số người ở phải từ 1 đến 20");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Mô tả không quá 500 ký tự");
        }
    }
}
