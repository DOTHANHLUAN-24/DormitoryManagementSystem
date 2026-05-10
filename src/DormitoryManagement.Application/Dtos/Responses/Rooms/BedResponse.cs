namespace DormitoryManagement.Application.Dtos.Responses.Rooms
{
    public class BedResponse
    {
        public Guid Id { get; set; }
        public string BedNumber { get; set; } = string.Empty;
        public bool IsOccupied { get; set; }
    }
}
