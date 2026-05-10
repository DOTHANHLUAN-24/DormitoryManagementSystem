using System.ComponentModel.DataAnnotations;

namespace DormitoryManagement.Application.Dtos.Requests.Rooms
{
    public class CreateRoomRequest
    {
        [Required(ErrorMessage = "Số phòng không được để trống")]
        [StringLength(20)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số tầng")]
        public int Floor { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn tòa nhà")]
        public Guid BlockId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại phòng")]
        public Guid RoomTypeId { get; set; }
    }
}
