namespace DormitoryManagement.Application.Dtos.Responses.Rooms
{
    // DTO hiển thị chi tiết phòng (Bao gồm các danh sách liên quan)
    public class RoomDetailResponse : RoomResponse
    {
        public List<BedResponse> Beds { get; set; } = new();
        public List<AssetResponse> Assets { get; set; } = new();
        public DateTime CreatedDate { get; set; }
    }
}
