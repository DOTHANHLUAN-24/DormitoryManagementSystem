using DormitoryManagement.Domain.Entities;
using DormitoryManagement.Domain.Enums;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    public interface IAssetRepository : IBaseRepository<Asset>
    {
        Task<Asset?> GetByAssetCodeAsync(string assetCode);

        Task<IEnumerable<Asset>> GetActiveAssetsByRoomIdAsync(Guid roomId);

        Task<bool> IsAssetActiveAsync(Guid assetId);

        // Lọc theo trạng thái tài sản
        Task<IEnumerable<Asset>> GetAssetsByStatusAsync(AssetStatus status);
    }
}
