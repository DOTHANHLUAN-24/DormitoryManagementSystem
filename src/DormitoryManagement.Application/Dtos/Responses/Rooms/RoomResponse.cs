namespace DormitoryManagement.Application.Dtos.Responses.Rooms
{
    public class RoomResponse
    {
        public Guid Id { get; set; }

        public string RoomNumber { get; set; } = null!;

        public int Floor { get; set; }

        public string Status { get; set; } = null!;

        public Guid BlockId { get; set; }

        public Guid RoomTypeId { get; set; }

        public string BlockName { get; set; } = string.Empty;

        public string RoomTypeName { get; set; } = string.Empty;

        public decimal BasePrice { get; set; }

        public int MaxOccupants { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}