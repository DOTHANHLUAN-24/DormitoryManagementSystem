using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.Vehicles;

namespace DormitoryManagement.Application.Validators.Vehicles
{
    public class VehicleRequestDtoValidator : AbstractValidator<VehicleRequestDto>
    {
        public VehicleRequestDtoValidator()
        {
            RuleFor(x => x.VehicleType)
                .NotEmpty().WithMessage("Loại phương tiện không được để trống")
                .MaximumLength(100).WithMessage("Loại phương tiện không được quá 100 ký tự");

            RuleFor(x => x.LicensePlate)
                .NotEmpty().WithMessage("Biển số không được để trống")
                .MaximumLength(30).WithMessage("Biển số không được quá 30 ký tự");

            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("Chủ sở hữu không được để trống");
        }
    }
}
