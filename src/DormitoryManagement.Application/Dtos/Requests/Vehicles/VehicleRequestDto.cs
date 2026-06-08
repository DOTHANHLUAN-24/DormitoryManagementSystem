using System;
using System.ComponentModel.DataAnnotations;

namespace DormitoryManagement.Application.Dtos.Requests.Vehicles
{
    /// <summary>
    /// DTO tạo mới phương tiện (Vehicle).
    /// </summary>
    public class VehicleRequestDto
    {
        [Required(ErrorMessage = "Loại phương tiện không được để trống")]
        [StringLength(100, ErrorMessage = "Loại phương tiện không được quá 100 ký tự")]
        public string VehicleType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Biển số không được để trống")]
        [StringLength(30, ErrorMessage = "Biển số không được quá 30 ký tự")]
        public string LicensePlate { get; set; } = string.Empty;

        [Required(ErrorMessage = "Chủ sở hữu không được để trống")]
        public Guid OwnerId { get; set; }

        public string Status { get; set; } = "Approved";

        public bool IsActive { get; set; } = true;
    }
}
