using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Requests.Rooms
{
    public class RoomFilterRequest
    {
        public string? SearchTerm { get; set; } // Tìm theo số phòng
        public Guid? BlockId { get; set; }
        public Guid? RoomTypeId { get; set; }
        public RoomStatus? Status { get; set; }

        // Tìm kiếm theo giá (Nghiệp vụ mới)
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
