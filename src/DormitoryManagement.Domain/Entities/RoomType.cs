using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    /// <summary>
    /// Thực thể đại diện cho loại phòng trong ký túc xá.
    /// Xác định mức giá thuê, sức chứa tối đa và mô tả cho từng hạng phòng.
    /// </summary>
    [Table("RoomTypes")]
    public class RoomType : BaseEntity
    {
        /// <summary>Tên loại phòng (ví dụ: Phòng đơn, Phòng đôi, Phòng tập thể). Tối đa 50 ký tự.</summary>
        [Required(ErrorMessage = "Tên loại phòng không được để trống")]
        [StringLength(50, ErrorMessage = "Tên loại phòng không được vượt quá 50 ký tự")]
        public string TypeName { get; set; } = string.Empty;

        /// <summary>Giá thuê cơ bản mỗi tháng của loại phòng này (đơn vị: VNĐ). Chưa bao gồm điện, nước và phụ phí.</summary>
        [Required(ErrorMessage = "Giá cơ bản không được để trống")]
        [Range(0, 1000000000, ErrorMessage = "Giá cơ bản phải lớn hơn hoặc bằng 0")]
        public decimal BasePrice { get; set; }

        /// <summary>Số lượng người tối đa được phép ở trong loại phòng này (tương đương số giường, từ 1 đến 20).</summary>
        [Required(ErrorMessage = "Sức chứa tối đa không được để trống")]
        [Range(1, 20, ErrorMessage = "Sức chứa phải từ 1 đến 20 người")]
        public int MaxOccupants { get; set; }

        /// <summary>Mô tả chi tiết về tiêu chuẩn, tiện nghi và đặc điểm của loại phòng.</summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>Danh sách các phòng thuộc loại phòng này.</summary>
        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
