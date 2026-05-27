using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho một tòa nhà (khu/block) trong ký túc xá.
    /// </summary>
    [Table("Blocks")]
    public class Block : BaseEntity
    {
        /// <summary>Tên tòa nhà hoặc khu (ví dụ: Khu A, Khu B, Tòa CT1). Tối đa 100 ký tự.</summary>
        [Required, StringLength(100)]
        public string BlockName { get; set; } = string.Empty;

        /// <summary>Tổng số tầng của tòa nhà (dùng để quản lý phân bổ phòng theo tầng).</summary>
        public int TotalFloors { get; set; }

        /// <summary>Mô tả bổ sung về tòa nhà (vị trí, tiện ích kèm theo, ghi chú quản lý...).</summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>Danh sách các phòng thuộc tòa nhà này.</summary>
        public virtual ICollection<Room> Rooms { get; set; } = [];
    }
}
