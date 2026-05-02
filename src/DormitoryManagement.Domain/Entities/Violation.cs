using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Domain.Entities
{
    // Vi phạm
    [Table("Violations")]
    public class Violation
    {
        [Key]
        public int Id { get; set; }
        
        public string Description { get; set; } = string.Empty;
        
        public decimal FineAmount { get; set; }
        
        public DateTime ViolationDate { get; set; }
        
        public ViolationStatus Status { get; set; }
        
        public string EvidenceImage { get; set; } = string.Empty;

        public int ContractId { get; set; }
        
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; } = null!;
    }
}
