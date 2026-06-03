using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.Rooms;

namespace DormitoryManagement.Application.Validators.Rooms
{
    public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
    {
        public CreateRoomRequestValidator()
        {
            RuleFor(x => x.RoomNumber)
                .NotEmpty().WithMessage("Số phòng không được để trống")
                .MaximumLength(20).WithMessage("Số phòng không được quá 20 ký tự");

            RuleFor(x => x.Floor)
                .GreaterThan(0).WithMessage("Tầng phải lớn hơn 0");

            RuleFor(x => x.BlockId)
                .NotEmpty().WithMessage("Vui lòng chọn tòa nhà");

            RuleFor(x => x.RoomTypeId)
                .NotEmpty().WithMessage("Vui lòng chọn loại phòng");
        }
    }
}
