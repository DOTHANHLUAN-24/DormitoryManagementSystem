using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    // Dịch vụ / tiện ích (Điện, nước, internet, phí giữ xe)
    [Table("Utilities")]
    public class Utility : BaseEntity
    {
        [Required, StringLength(100)]
        public string UtilityName { get; set; } = string.Empty; // Điện, Nước...
        
        public decimal UnitPrice { get; set; }
        
        public string Unit { get; set; } = string.Empty;
    }
}
