using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể quản lý chi tiết từng giường trong phòng ký túc xá.
    /// </summary>
    [Table("Beds")]
    public class Bed : BaseEntity
    {
        /// <summary>Số hiệu giường trong phòng (ví dụ: G01, G02). Dùng để phân biệt các giường trong cùng một phòng.</summary>
        [Required, StringLength(20)]
        public string BedNumber { get; set; } = string.Empty;

        /// <summary>Trạng thái sử dụng của giường (Available = Trống, Occupied = Có người ở, UnderMaintenance = Bảo trì).</summary>
        public BedStatus Status { get; set; } = BedStatus.Available;

        /// <summary>Khóa ngoại trỏ đến phòng chứa giường này.</summary>
        public Guid RoomId { get; set; }

        /// <summary>Navigation property đến phòng chứa giường.</summary>
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; } = null!;

        /// <summary>Danh sách hợp đồng thuê đã từng gắn với giường này (bao gồm lịch sử).</summary>
        public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
    }
}
