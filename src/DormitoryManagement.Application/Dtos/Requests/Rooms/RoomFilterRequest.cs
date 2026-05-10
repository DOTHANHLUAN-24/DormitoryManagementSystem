using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.Rooms
{
    public class RoomFilterRequest
    {
        public string? SearchTerm { get; set; }
        public Guid? BlockId { get; set; }
        public Guid? RoomTypeId { get; set; }
        public RoomStatus? Status { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
