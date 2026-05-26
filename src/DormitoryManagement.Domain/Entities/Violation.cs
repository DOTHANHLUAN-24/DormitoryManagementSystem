using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể ghi nhận các vi phạm nội quy của sinh viên.
    /// </summary>
    [Table("Violations")]
    public class Violation : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        
        public decimal FineAmount { get; set; }
        
        public DateTime ViolationDate { get; set; }
        
        public ViolationStatus Status { get; set; }
        
        public string EvidenceImage { get; set; } = string.Empty;

        /// <summary>Ghi chú khi xử lý vi phạm (nhân viên ghi lại biện pháp, kết quả)</summary>
        public string? ResolveNote { get; set; }

        /// <summary>Thời gian xử lý xong vi phạm</summary>
        public DateTime? ResolvedAt { get; set; }

        public Guid ContractId { get; set; }
        
        [ForeignKey("ContractId")]
        public virtual Contract Contract { get; set; } = null!;
    }
}
