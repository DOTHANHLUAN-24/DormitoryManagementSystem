using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Enums;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho một phòng trong ký túc xá.
    /// Mỗi phòng thuộc một tòa nhà (Block) và có một loại phòng (RoomType) xác định giá và sức chứa.
    /// </summary>
    [Table("Rooms")]
    public class Room : BaseEntity
    {
        /// <summary>Số phòng dùng để định danh (ví dụ: P101, A-202). Tối đa 20 ký tự.</summary>
        [Required, StringLength(20)]
        public string RoomNumber { get; set; } = string.Empty;

        /// <summary>Tầng mà phòng tọa lạc trong tòa nhà (ví dụ: 1, 2, 3...).</summary>
        public int Floor { get; set; }

        /// <summary>Trạng thái sử dụng của phòng (Available = Còn trống, Full = Đầy, UnderMaintenance = Đang bảo trì).</summary>
        public RoomStatus Status { get; set; } = RoomStatus.Available;

        /// <summary>Khóa ngoại trỏ đến tòa nhà (Block) chứa phòng này.</summary>
        public Guid BlockId { get; set; }

        /// <summary>Khóa ngoại trỏ đến loại phòng (RoomType) xác định giá và sức chứa.</summary>
        public Guid RoomTypeId { get; set; }

        /// <summary>Danh sách các giường trong phòng.</summary>
        public virtual ICollection<Bed> Beds { get; set; } = new List<Bed>();

        /// <summary>Danh sách lịch sử ghi nhận tiêu thụ điện/nước của phòng.</summary>
        public virtual ICollection<UtilityUsage> UtilityUsages { get; set; } = new List<UtilityUsage>();

        /// <summary>Danh sách tài sản/trang thiết bị được trang bị trong phòng.</summary>
        public virtual ICollection<Asset> Assets { get; set; } = new List<Asset>();

        /// <summary>Navigation property đến tòa nhà chứa phòng này.</summary>
        [ForeignKey("BlockId")]
        public virtual Block Block { get; set; } = null!;

        /// <summary>Navigation property đến loại phòng xác định giá và tiêu chuẩn.</summary>
        [ForeignKey("RoomTypeId")]
        public virtual RoomType RoomType { get; set; } = null!;
    }
}
