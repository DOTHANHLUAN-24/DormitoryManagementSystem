using FluentValidation;
using DormitoryManagement.Application.Dtos.Requests.Violations;

namespace DormitoryManagement.Application.Validators.Violations
{
    public class ViolationRequestDtoValidator : AbstractValidator<ViolationRequestDto>
    {
        public ViolationRequestDtoValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("Mã số sinh viên không được để trống");

            RuleFor(x => x.Room)
                .NotEmpty().WithMessage("Vui lòng nhập tên phòng ở");

            RuleFor(x => x.Severity)
                .NotEmpty().WithMessage("Vui lòng chọn mức độ vi phạm");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Vui lòng chọn ngày lập biên bản")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày lập biên bản không được lớn hơn ngày hiện tại");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Nội dung vi phạm không được để trống");
        }
    }
}
