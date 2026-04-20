using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    // Dịch vụ / tiện ích (Điện, nước, internet, phí giữ xe)
    [Table("Utilities")]
    public class Utility
    {
        [Key]
        public int Id { get; set; }
        
        [Required, StringLength(100)]
        
        public string UtilityName { get; set; } = string.Empty; // Điện, Nước...
        
        public decimal UnitPrice { get; set; }
        
        public string Unit { get; set; } = string.Empty;
        
        public bool IsActive { get; set; } = true;
    }
}
