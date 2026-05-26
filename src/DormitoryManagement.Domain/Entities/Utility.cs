using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho dịch vụ hoặc tiện ích (Điện, nước, internet...).
    /// </summary>
    [Table("Utilities")]
    public class Utility : BaseEntity
    {
        [Required, StringLength(100)]
        public string UtilityName { get; set; } = string.Empty; // Điện, Nước...
        
        public decimal UnitPrice { get; set; }
        
        public string Unit { get; set; } = string.Empty;
    }
}
