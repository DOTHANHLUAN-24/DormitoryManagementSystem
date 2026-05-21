using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DormitoryManagement.Domain.Interfaces.Entities;

namespace DormitoryManagement.Domain.Entities
{
    // Loại phòng
    [Table("RoomTypes")]
    public class RoomType : BaseEntity
    {
        [Required(ErrorMessage = "Tên loại phòng không được để trống")]
        [StringLength(50, ErrorMessage = "Tên loại phòng không được vượt quá 50 ký tự")]
        public string TypeName { get; set; } = string.Empty; // Ví dụ: Phòng đơn, Phòng đôi, Phòng tập thể

        [Required(ErrorMessage = "Giá cơ bản không được để trống")]
        [Range(0, 1000000000, ErrorMessage = "Giá cơ bản phải lớn hơn hoặc bằng 0")]
        public decimal BasePrice { get; set; }

        [Required(ErrorMessage = "Sức chứa tối đa không được để trống")]
        [Range(1, 20, ErrorMessage = "Sức chứa phải từ 1 đến 20 người")]
        public int MaxOccupants { get; set; } // Bằng số lượng giường trong phòng (4 -> 6)

        public string Description { get; set; } = string.Empty;

        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
