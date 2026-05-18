using System.ComponentModel.DataAnnotations;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.Rooms
{
    public class CreateRoomRequest
    {
<<<<<<< HEAD
        [Required(ErrorMessage = "Số phòng không được để trống")]
        [StringLength(20)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public int Floor { get; set; }

        [Required]
        public Guid BlockId { get; set; }

        [Required]
=======
        [Required(ErrorMessage = "Số phòng là bắt buộc")]
        public string RoomNumber { get; set; } = null!;
        public int Floor { get; set; }
        public RoomStatus Status { get; set; } = RoomStatus.Available;
        public Guid BlockId { get; set; }
>>>>>>> 5cec099004cb5aaad701cbbafc6733fbc20d4002
        public Guid RoomTypeId { get; set; }

        public RoomStatus Status { get; set; } = RoomStatus.Available;
    }
}