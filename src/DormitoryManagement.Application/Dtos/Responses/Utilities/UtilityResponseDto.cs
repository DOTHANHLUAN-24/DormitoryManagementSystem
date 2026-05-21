using System;

namespace DormitoryManagement.Application.Dtos.Responses.Utilities
{
    public class UtilityResponseDto
    {
        public Guid Id { get; set; }
        public string UtilityName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string Unit { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }

        public string FormattedUnitPrice => UnitPrice.ToString("N0") + " VNĐ";
    }
}
