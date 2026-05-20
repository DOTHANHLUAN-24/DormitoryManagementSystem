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
    }
}
