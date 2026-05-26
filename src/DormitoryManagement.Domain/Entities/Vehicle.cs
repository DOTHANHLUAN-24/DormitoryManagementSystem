using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể quản lý phương tiện cá nhân của sinh viên/nhân viên.
    /// </summary>
    [Table("Vehicles")]
    public class Vehicle : BaseEntity
    {
        public string VehicleType { get; set; } = string.Empty;
        
        public string LicensePlate { get; set; } = string.Empty;

        public Guid OwnerId { get; set; }
        
        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; } = null!;
    }
}
