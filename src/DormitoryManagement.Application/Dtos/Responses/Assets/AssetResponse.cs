namespace DormitoryManagement.Application.Dtos.Responses.Assets
{
    public class AssetResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
