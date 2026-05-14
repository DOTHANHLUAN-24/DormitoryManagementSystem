using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Application.Dtos.Responses.Rooms
{
    public class RoomResponse
    {
        public Guid Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public int Floor { get; set; }
        public RoomStatus Status { get; set; } // Chuyển Enum sang String/Description

        // Thông tin từ các bảng liên kết (Chỉ lấy Name để UI hiển thị)
        public Guid BlockId { get; set; }
        public string BlockName { get; set; } = string.Empty;

        public Guid RoomTypeId { get; set; }
        public string RoomTypeName { get; set; } = string.Empty;
        public decimal Price { get; set; } // Lấy từ RoomType

        // Thống kê nhanh
        public int TotalBeds { get; set; }
        public int AvailableBeds { get; set; }
    }
}
