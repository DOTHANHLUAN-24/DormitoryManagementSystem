using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    // Phương tiện
    [Table("Vehicles")]
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
       
        public string VehicleType { get; set; } = string.Empty;
        
        public string LicensePlate { get; set; } = string.Empty;

        public string OwnerId { get; set; } = string.Empty;
        
        [ForeignKey("OwnerId")]
        public virtual User Owner { get; set; } = null!;
    }
}
