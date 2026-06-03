using FluentValidation;
using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Validators.Entities
{
    public class ViolationValidator : AbstractValidator<Violation>
    {
        public ViolationValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Mô tả vi phạm không được để trống");

            RuleFor(x => x.FineAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Số tiền phạt không hợp lệ");

            RuleFor(x => x.ViolationDate)
                .NotEmpty().WithMessage("Ngày vi phạm không được để trống")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày vi phạm không được lớn hơn ngày hiện tại");

            RuleFor(x => x.ContractId)
                .NotEmpty().WithMessage("Hợp đồng không được để trống");

            RuleFor(x => x.ResolveNote)
                .MaximumLength(500).WithMessage("Ghi chú xử lý không được quá 500 ký tự")
                .When(x => !string.IsNullOrEmpty(x.ResolveNote));
        }
    }
}
