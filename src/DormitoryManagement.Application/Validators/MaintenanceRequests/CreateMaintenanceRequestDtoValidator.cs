using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.MaintenanceRequests;

namespace DormitoryManagement.Application.Validators.MaintenanceRequests
{
    public class CreateMaintenanceRequestDtoValidator : AbstractValidator<CreateMaintenanceRequestDto>
    {
        public CreateMaintenanceRequestDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Tiêu đề không được để trống")
                .MaximumLength(200).WithMessage("Tiêu đề không được vượt quá 200 ký tự");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Mô tả không được để trống");

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("Phòng không được để trống");
        }
    }
}
