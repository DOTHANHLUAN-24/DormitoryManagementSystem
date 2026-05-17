
namespace DormitoryManagement.Application.Dtos.Requests.Rooms
{
    public class RoomStatisticsDto
    {
        public int TotalRooms { get; set; }
        public int AvailableRooms { get; set; }
        public int OccupiedRooms { get; set; } // Tương ứng với Status "Full"
        public int MaintenanceRooms { get; set; }
    }
}