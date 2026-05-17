namespace DormitoryManagement.Application.Dtos.Responses.Blocks
{
    public class BlockResponseDto
    {
        public Guid Id { get; set; }
        public string BlockName { get; set; } = string.Empty;
        public int TotalFloors { get; set; }
        public string Description { get; set; } = string.Empty;

        // Thông tin audit
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        // Thêm thông tin đếm số lượng phòng (tùy chọn)
        public int RoomCount { get; set; }
    }
}