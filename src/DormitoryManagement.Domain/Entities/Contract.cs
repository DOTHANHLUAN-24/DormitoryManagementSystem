using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Domain.Entities
{
    // Hợp đồng
    [Table("Contracts")]
    public class Contract
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string ContractCode { get; set; } = string.Empty; // Ví dụ: HD001, HD002

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal DepositAmount { get; set; }

        public ContractStatus Status { get; set; } = ContractStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        // Map với User và Bed thay vì Room
        public string UserId { get; set; } = null!;
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        public int BedId { get; set; }

        [ForeignKey("BedId")]
        public virtual Bed Bed { get; set; } = null!;

        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

        public virtual ICollection<Violation> Violations { get; set; } = new List<Violation>();
    }
}
