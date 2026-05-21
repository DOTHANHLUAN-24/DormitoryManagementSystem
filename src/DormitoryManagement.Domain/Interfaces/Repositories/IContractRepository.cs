using DormitoryManagement.Domain.Entities;

namespace DormitoryManagement.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Giao diện Repository quản lý thông tin hợp đồng thuê phòng (Contract).
    /// </summary>
    public interface IContractRepository : IBaseRepository<Contract>
    {
        /// <summary>
        /// Tìm kiếm hợp đồng theo mã hợp đồng (ContractCode).
        /// </summary>
        /// <param name="contractCode">Mã hợp đồng cần tìm</param>
        /// <returns>Hợp đồng nếu tìm thấy, ngược lại là null</returns>
        Task<Contract?> GetByContractCodeAsync(string contractCode);

        /// <summary>
        /// Lấy đối tượng truy vấn phân trang và tìm kiếm hợp đồng theo từ khóa.
        /// </summary>
        /// <param name="searchString">Từ khóa tìm kiếm (mã hợp đồng, tên sinh viên...)</param>
        /// <returns>Đối tượng IQueryable chứa danh sách hợp đồng tìm được</returns>
        IQueryable<Contract> GetPagingQuery(string searchString);

        /// <summary>
        /// Lấy danh sách hợp đồng của một người dùng (sinh viên).
        /// </summary>
        /// <param name="userId">Id của người dùng</param>
        /// <returns>Danh sách hợp đồng của người dùng</returns>
        Task<IEnumerable<Contract>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// Lấy hợp đồng đang hoạt động liên kết với một giường cụ thể.
        /// </summary>
        /// <param name="bedId">Id của giường cần kiểm tra</param>
        /// <returns>Thông tin hợp đồng tương ứng</returns>
        Task<Contract> GetByBedIdAsync(Guid bedId);
    }
}
