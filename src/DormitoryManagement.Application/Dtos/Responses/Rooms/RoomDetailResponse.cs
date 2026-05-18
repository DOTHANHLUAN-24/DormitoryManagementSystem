using DormitoryManagement.Application.Dtos.Responses.Assets;
using DormitoryManagement.Application.Dtos.Responses.Beds;

namespace DormitoryManagement.Application.Dtos.Responses.Rooms
{
    public class RoomDetailResponse : RoomResponse
    {
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }

        public List<BedResponse> Beds { get; set; } = new();

        public List<AssetResponse> Assets { get; set; } = new();
    }
}