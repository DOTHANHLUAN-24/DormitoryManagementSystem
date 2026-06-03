using FluentValidation;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Validators.Entities
{
    public class RoomValidator : AbstractValidator<Room>
    {
        public RoomValidator()
        {
            RuleFor(x => x.RoomNumber)
                .NotEmpty().WithMessage("Số phòng không được để trống")
                .MaximumLength(20).WithMessage("Số phòng không được quá 20 ký tự");

            RuleFor(x => x.Floor)
                .GreaterThan(0).WithMessage("Tầng phải lớn hơn 0");

            RuleFor(x => x.BlockId)
                .NotEmpty().WithMessage("Tòa nhà không được để trống");

            RuleFor(x => x.RoomTypeId)
                .NotEmpty().WithMessage("Loại phòng không được để trống");
        }
    }
}
