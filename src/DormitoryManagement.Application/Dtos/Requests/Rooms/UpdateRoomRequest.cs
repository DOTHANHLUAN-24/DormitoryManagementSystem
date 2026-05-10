using System.ComponentModel.DataAnnotations;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.Rooms
{
    public class UpdateRoomRequest
    {
        [Required]
        public string RoomNumber { get; set; } = string.Empty;

        public int Floor { get; set; }

        public RoomStatus Status { get; set; }

        public Guid BlockId { get; set; }

        public Guid RoomTypeId { get; set; }
    }
}
