using FluentValidation;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Validators.Entities
{
    public class MaintenanceRequestValidator : AbstractValidator<MaintenanceRequest>
    {
        public MaintenanceRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Tiêu đề không được để trống")
                .MaximumLength(200).WithMessage("Tiêu đề không được quá 200 ký tự");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Mô tả không được để trống");

            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("Phòng không được để trống");

            RuleFor(x => x.RequesterId)
                .NotEmpty().WithMessage("Người gửi yêu cầu không được để trống");

            RuleFor(x => x.HandlerId)
                .NotNull().WithMessage("Nhân viên xử lý không hợp lệ")
                .When(x => x.HandlerId.HasValue);
        }
    }
}
