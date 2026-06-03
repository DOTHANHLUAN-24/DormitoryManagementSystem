using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.MaintenanceRequests;

namespace DormitoryManagement.Application.Validators.MaintenanceRequests
{
    public class UpdateMaintenanceStatusDtoValidator : AbstractValidator<UpdateMaintenanceStatusDto>
    {
        public UpdateMaintenanceStatusDtoValidator()
        {
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Trạng thái không hợp lệ");
        }
    }
}
