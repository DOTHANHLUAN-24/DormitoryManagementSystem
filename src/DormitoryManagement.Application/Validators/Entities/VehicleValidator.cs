using FluentValidation;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Validators.Entities
{
    public class VehicleValidator : AbstractValidator<Vehicle>
    {
        public VehicleValidator()
        {
            RuleFor(x => x.VehicleType)
                .NotEmpty().WithMessage("Loại phương tiện không được để trống")
                .MaximumLength(100).WithMessage("Loại phương tiện không được quá 100 ký tự");

            RuleFor(x => x.LicensePlate)
                .NotEmpty().WithMessage("Biển số xe không được để trống")
                .MaximumLength(30).WithMessage("Biển số xe không được quá 30 ký tự");

            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("Chủ sở hữu không được để trống");
        }
    }
}
