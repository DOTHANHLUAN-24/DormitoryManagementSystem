using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Application.Services.Interfaces
{
    /// <summary>
    /// Giao diện dịch vụ quản lý nghiệp vụ hợp đồng thuê phòng (Contract).
    /// </summary>
    public interface IContractService
    {
        /// <summary>
        /// Tìm kiếm hợp đồng theo mã hợp đồng (ContractCode).
        /// </summary>
        /// <param name="contractCode">Mã hợp đồng cần tìm</param>
        /// <returns>Hợp đồng tương ứng hoặc null nếu không tìm thấy</returns>
        Task<Contract?> GetByContractCodeAsync(string contractCode);

        /// <summary>
        /// Lấy danh sách tất cả hợp đồng của một người dùng cụ thể.
        /// </summary>
        /// <param name="userId">Id của người dùng (sinh viên)</param>
        /// <returns>Danh sách hợp đồng</returns>
        Task<IEnumerable<Contract>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Lấy hợp đồng đang hoạt động liên kết với giường cụ thể.
        /// </summary>
        /// <param name="bedId">Id của giường</param>
        /// <returns>Hợp đồng thuê giường đó</returns>
        Task<Contract> GetByBedIdAsync(Guid bedId);

        /// <summary>
        /// Lấy hợp đồng theo Id.
        /// </summary>
        Task<Contract?> GetByIdAsync(Guid id);

        /// <summary>
        /// Lấy danh sách hợp đồng phân trang, hỗ trợ tìm kiếm và lọc theo trạng thái.
        /// </summary>
        Task<DormitoryManagement.Domain.Common.PagedResult<Contract>> GetPagedContractsAsync(int pageIndex, int pageSize, string? searchString = null, DormitoryManagement.Domain.Enums.ContractStatus? status = null);

        /// <summary>
        /// Tạo mới một hợp đồng.
        /// </summary>
        Task<bool> CreateContractAsync(Contract contract);

        /// <summary>
        /// Cập nhật thông tin hợp đồng.
        /// </summary>
        Task<bool> UpdateContractAsync(Contract contract);

        /// <summary>
        /// Xóa hợp đồng theo Id.
        /// </summary>
        Task<bool> DeleteContractAsync(Guid id);

        /// <summary>
        /// Lấy số lượng hợp đồng ở trạng thái chờ duyệt (Pending).
        /// </summary>
        Task<int> GetPendingCountAsync();
    }
}
