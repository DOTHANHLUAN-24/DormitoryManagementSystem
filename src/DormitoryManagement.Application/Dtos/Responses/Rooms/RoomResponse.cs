namespace DormitoryManagement.Application.Dtos.Responses.Rooms
{
    public class RoomResponse
    {
        public Guid Id { get; set; }
<<<<<<< HEAD
        public string RoomNumber { get; set; } = string.Empty;
        public int Floor { get; set; }
        public RoomStatus Status { get; set; }

        // Hiển thị tên Enum tiếng Việt hoặc string nếu cần
        public string StatusDisplay => Status.ToString();

        public Guid BlockId { get; set; }
        public string? BlockName { get; set; } // Lấy từ virtual Block

        public Guid RoomTypeId { get; set; }
        public string? RoomTypeName { get; set; } // Lấy từ virtual RoomType

        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
=======

        public string RoomNumber { get; set; } = null!;

        public int Floor { get; set; }

        public string Status { get; set; } = null!;

        public Guid BlockId { get; set; }

        public Guid RoomTypeId { get; set; }

        public string BlockName { get; set; } = string.Empty;

        public string RoomTypeName { get; set; } = string.Empty;

        public decimal BasePrice { get; set; }

        public int MaxOccupants { get; set; }
>>>>>>> 5cec099004cb5aaad701cbbafc6733fbc20d4002
    }
}