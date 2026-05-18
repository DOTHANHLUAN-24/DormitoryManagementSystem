namespace DormitoryManagement.Application.Dtos.Responses.Blocks
{
    public class BlockResponseDto
    {
        public Guid Id { get; set; }

        public string BlockName { get; set; } = string.Empty;

        public int TotalFloors { get; set; }

        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public int TotalRooms { get; set; }
    }
}