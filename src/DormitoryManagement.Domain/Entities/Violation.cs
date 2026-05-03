using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    // Vi phạm
    [Table("Violations")]
    public class Violation : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        
        public decimal FineAmount { get; set; }
        
        public DateTime ViolationDate { get; set; }
        
        public ViolationStatus Status { get; set; }
        
        public string EvidenceImage { get; set; } = string.Empty;

        public Guid ContractId { get; set; }
        
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; } = null!;
    }
}
