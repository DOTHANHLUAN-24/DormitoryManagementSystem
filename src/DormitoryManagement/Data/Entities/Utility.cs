using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DormitoryManagement.Data.Entities
{
    [Table("Utilities")]
    public class Utility
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        
        public string Name { get; set; } = string.Empty;

        public decimal UnitPrice { get; set; }
        
        public string Unit { get; set; } = string.Empty;
    }
}
