using System.ComponentModel.DataAnnotations;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.Rooms
{
    public class CreateRoomRequest
    {
        [Required(ErrorMessage = "Số phòng không được để trống")]
        [StringLength(20)]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public int Floor { get; set; }

        [Required]
        public Guid BlockId { get; set; }

        [Required]
        public Guid RoomTypeId { get; set; }

        public RoomStatus Status { get; set; } = RoomStatus.Available;
    }
}