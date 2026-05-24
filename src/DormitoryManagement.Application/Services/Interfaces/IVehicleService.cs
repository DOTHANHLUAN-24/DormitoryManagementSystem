using DormitoryManagement.Application.Dtos.Requests.Vehicles;
using DormitoryManagement.Application.Dtos.Responses.Vehicles;
using DormitoryManagement.Domain.Common;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý phương tiện (Vehicle).
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Lấy danh sách phương tiện phân trang, hỗ trợ tìm kiếm theo biển số/loại xe,
        /// lọc theo trạng thái hoạt động và trạng thái xóa, có thể lọc theo Owner.
        /// </summary>
        Task<PagedResult<VehicleResponseDto>> GetPagedVehiclesAsync(
            int pageIndex,
            int pageSize,
            string? searchTerm,
            bool? isActive = null,
            bool? isDeleted = false,
            Guid? ownerId = null);

        /// <summary>
        /// Lấy danh sách phương tiện đang hoạt động của một chủ sở hữu.
        /// </summary>
        Task<IEnumerable<VehicleResponseDto>> GetActiveVehiclesByOwnerIdAsync(Guid ownerId);

        /// <summary>
        /// Lấy thông tin phương tiện theo Id.
        /// </summary>
        Task<VehicleResponseDto?> GetVehicleByIdAsync(Guid id);

        /// <summary>
        /// Lấy thông tin phương tiện theo biển số (LicensePlate).
        /// </summary>
        Task<VehicleResponseDto?> GetVehicleByLicensePlateAsync(string licensePlate);

        /// <summary>
        /// Tạo mới phương tiện.
        /// </summary>
        Task<bool> CreateVehicleAsync(VehicleRequestDto request);

        /// <summary>
        /// Cập nhật thông tin phương tiện.
        /// </summary>
        Task<bool> UpdateVehicleAsync(Guid id, VehicleUpdateDto request);

        /// <summary>
        /// Bật/tắt trạng thái hoạt động của phương tiện.
        /// </summary>
        Task<bool> ToggleVehicleStatusAsync(Guid id);

        /// <summary>
        /// Xóa mềm phương tiện (IsDeleted = true).
        /// </summary>
        Task<bool> SoftDeleteVehicleAsync(Guid id);

        /// <summary>
        /// Khôi phục phương tiện từ thùng rác (IsDeleted = false).
        /// </summary>
        Task<bool> RestoreVehicleAsync(Guid id);

        /// <summary>
        /// Xóa vĩnh viễn phương tiện khỏi database.
        /// </summary>
        Task<bool> DeletePermanentlyAsync(Guid id);

        /// <summary>
        /// Kiểm tra trùng biển số trong hệ thống.
        /// </summary>
        Task<bool> IsLicensePlateDuplicateAsync(string licensePlate, Guid? excludeId = null);
    }
}
