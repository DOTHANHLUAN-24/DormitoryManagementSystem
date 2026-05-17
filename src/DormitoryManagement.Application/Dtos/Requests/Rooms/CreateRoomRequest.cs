using System.ComponentModel.DataAnnotations;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.Rooms
{
    public class CreateRoomRequest
    {
        [Required(ErrorMessage = "Số phòng là bắt buộc")]
        public string RoomNumber { get; set; } = null!;
        public int Floor { get; set; }
        public RoomStatus Status { get; set; } = RoomStatus.Available;
        public Guid BlockId { get; set; }
        public Guid RoomTypeId { get; set; }
    }
}