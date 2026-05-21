using System;

namespace DormitoryManagement.Application.Dtos.Responses.Assets
{
    public class AssetResponse
    {
        public Guid Id { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public string AssetCode { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal ReplacementCost { get; set; }
        public Guid RoomId { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string BlockName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }   
        public DateTime? LastModified { get; set; }
    }
}
