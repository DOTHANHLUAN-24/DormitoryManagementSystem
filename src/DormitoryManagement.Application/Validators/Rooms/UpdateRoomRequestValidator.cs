using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.Rooms;

namespace DormitoryManagement.Application.Validators.Rooms
{
    public class UpdateRoomRequestValidator : AbstractValidator<UpdateRoomRequest>
    {
        public UpdateRoomRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id phòng không được để trống");

            Include(new CreateRoomRequestValidator());
        }
    }
}
