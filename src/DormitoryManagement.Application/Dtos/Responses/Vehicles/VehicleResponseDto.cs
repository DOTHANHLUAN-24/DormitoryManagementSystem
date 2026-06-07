using System;

namespace DormitoryManagement.Application.Dtos.Responses.Vehicles
{
    /// <summary>
    /// DTO trả về thông tin phương tiện (Vehicle).
    /// </summary>
    public class VehicleResponseDto
    {
        public Guid Id { get; set; }

        public string VehicleType { get; set; } = string.Empty;

        public string LicensePlate { get; set; } = string.Empty;

        public Guid OwnerId { get; set; }

        public string OwnerFullName { get; set; } = string.Empty;

        public string OwnerCode { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public string Status { get; set; } = "Pending";

        public DateTime CreatedDate { get; set; }

        public DateTime? LastModified { get; set; }
    }
}
