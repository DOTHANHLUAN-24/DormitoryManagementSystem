using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin phương tiện (Vehicle).
    /// </summary>
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        /// <summary>
        /// Lấy thông tin phương tiện theo biển số (LicensePlate).
        /// </summary>
        /// <param name="licensePlate">Biển số cần tìm</param>
        /// <returns>Phương tiện nếu tìm thấy, ngược lại là null</returns>
        Task<Vehicle?> GetByLicensePlateAsync(string licensePlate);

        /// <summary>
        /// Lấy danh sách phương tiện đang hoạt động thuộc về chủ sở hữu (OwnerId).
        /// </summary>
        /// <param name="ownerId">Id của chủ sở hữu</param>
        /// <returns>Danh sách phương tiện</returns>
        Task<IEnumerable<Vehicle>> GetActiveVehiclesByOwnerIdAsync(Guid ownerId);

        /// <summary>
        /// Kiểm tra phương tiện có đang hoạt động và chưa bị xóa không.
        /// </summary>
        /// <param name="vehicleId">Id của phương tiện</param>
        /// <returns>True nếu đang hoạt động và chưa bị xóa, ngược lại là False</returns>
        Task<bool> IsVehicleActiveAsync(Guid vehicleId);

        /// <summary>
        /// Kiểm tra trùng biển số trong hệ thống (tránh trùng lặp khi thêm/sửa).
        /// </summary>
        /// <param name="licensePlate">Biển số cần kiểm tra</param>
        /// <param name="excludeId">Id phương tiện loại trừ khi cập nhật (tùy chọn)</param>
        /// <returns>True nếu trùng với phương tiện khác, ngược lại là False</returns>
        Task<bool> IsLicensePlateDuplicateAsync(string licensePlate, Guid? excludeId = null);
    }
}
