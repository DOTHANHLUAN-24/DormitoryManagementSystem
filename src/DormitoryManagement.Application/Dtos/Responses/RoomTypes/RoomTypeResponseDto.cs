namespace DormitoryManagement.Application.Dtos.Responses.RoomTypes
{
    public class RoomTypeResponseDto
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public int MaxOccupants { get; set; }
        public string Description { get; set; } = string.Empty;

        // Hiển thị định dạng tiền tệ nếu cần
        public string FormattedBasePrice => BasePrice.ToString("N0") + " VNĐ";
    }
}