using System.ComponentModel.DataAnnotations;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.Rooms
{
    public class UpdateRoomRequest : CreateRoomRequest
    {
        public Guid Id { get; set; }
<<<<<<< HEAD

        [Required(ErrorMessage = "Số phòng không được để trống")]
        [StringLength(20, ErrorMessage = "Số phòng không được vượt quá 20 ký tự")]
        [Display(Name = "Số phòng")]
        public string RoomNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn tầng")]
        [Range(1, 100, ErrorMessage = "Số tầng không hợp lệ")]
        [Display(Name = "Tầng")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn trạng thái")]
        [Display(Name = "Trạng thái")]
        public RoomStatus Status { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tòa nhà (Block)")]
        [Display(Name = "Tòa nhà")]
        public Guid BlockId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại phòng")]
        [Display(Name = "Loại phòng")]
        public Guid RoomTypeId { get; set; }

        [Display(Name = "Trạng thái hoạt động")]
        public bool IsActive { get; set; }
=======
>>>>>>> 5cec099004cb5aaad701cbbafc6733fbc20d4002
    }
}