using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin dịch vụ / tiện ích (Utility).
    /// </summary>
    public interface IUtilityRepository : IBaseRepository<Utility>
    {
        /// <summary>
        /// Lấy thông tin dịch vụ theo tên dịch vụ (UtilityName).
        /// </summary>
        /// <param name="utilityName">Tên dịch vụ tiện ích</param>
        /// <returns>Dịch vụ tiện ích nếu tìm thấy, ngược lại là null</returns>
        Task<Utility?> GetByUtilityNameAsync(string utilityName);

        /// <summary>
        /// Lấy danh sách tất cả dịch vụ tiện ích đang hoạt động.
        /// </summary>
        /// <returns>Danh sách các dịch vụ tiện ích đang hoạt động</returns>
        Task<IEnumerable<Utility>> GetActiveUtilitiesAsync();

        /// <summary>
        /// Kiểm tra xem một dịch vụ tiện ích có đang hoạt động hay không.
        /// </summary>
        /// <param name="utilityId">Id của dịch vụ tiện ích</param>
        /// <returns>True nếu dịch vụ đang hoạt động, ngược lại là False</returns>
        Task<bool> IsUtilityActiveAsync(Guid utilityId);
    }
}
